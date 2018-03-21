﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RotationSpeed;
    [SerializeField]
    private int m_Oxygen;
    [SerializeField]
    private float m_OxygenConsumeTimer;
    [SerializeField]
    private GameObject m_CameraHolder;

    private List<GameObject> m_CollectedTrash;

    private Rigidbody m_Rigidbody;

    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private LayerMask m_Layer;

    private int m_MaxOxygen;
    private float m_MaxDepth;
    private int m_Money;
    private int m_MaxWeight;
    private int m_CurrentWeight;

    private int m_State;

    void Start()
    {
        m_CollectedTrash = new List<GameObject>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 1.5f;
        m_RotationSpeed = 50f;
        m_Oxygen = 30;
        m_OxygenConsumeTimer = 3f;
        m_MaxDepth = 10f;
        m_Money = 0;
    }

	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forwardVector = transform.rotation * Vector3.forward;
            transform.position += forwardVector * (Time.deltaTime * m_Speed);
            m_State = 1;
        }
        else
        {
            m_State = 0;
        }

        //transform.position += new Vector3(Input.GetAxis("Horizontal") * m_Speed, 0, Input.GetAxis("Vertical") * m_Speed);
        ConsomeOxygen();
        SetCamera();
        m_Animator.SetInteger("State", m_State);
    }

    public void AddGold(int amount)
    {
        m_Money += amount;
    }

    public bool RemoveGold(int amount)
    {
        bool canBuy = false;

        if (m_Money >= amount)
        {
            m_Money -= amount;
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }

        return canBuy;
    }

    private void ConsomeOxygen()
    {
        if (m_OxygenConsumeTimer <= 0f)
        {
            m_Oxygen -= 3;
            m_OxygenConsumeTimer = 3f;
        }
    }

    public List<GameObject> SellTrash()
    {
        List<GameObject> trash = m_CollectedTrash;
        m_CollectedTrash.Clear();

        return trash;
    }

    public void UpgradeAirTank(int muliplier)
    {
        m_MaxOxygen = 30 * muliplier;
        ResetOxygen();
    }

    public void UpgradeWeightLimit(int amount)
    {
        m_MaxWeight += amount;
    }

    public void UpgradeSpeed(int amount)
    {
        m_Speed += amount;
    }

    public void ResetOxygen()
    {
        m_Oxygen = m_MaxOxygen;
    }

    private void SetCamera()
    {
        m_CameraHolder.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));

        Physics.Raycast(ray, out hit, Mathf.Infinity, m_Layer);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 50, Color.red);

        Vector3 dir = (hit.point - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.CompareTag("Trash"))
        {
            m_CollectedTrash.Add(collidedObject.gameObject);
        }
    }
}
