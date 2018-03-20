using System.Collections;
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

    private int m_MaxOxygen;
    private float m_MaxDepth;
    private float m_CurrentDepth;

    private List<GameObject> m_CollectedTrash;

    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_CollectedTrash = new List<GameObject>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 1.2f;
        m_RotationSpeed = 50f;
        m_Oxygen = 30;
        m_OxygenConsumeTimer = 3f;
        m_MaxDepth = 10f;
    }

	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forwardVector = transform.rotation * Vector3.forward;
            transform.position += forwardVector * (Time.deltaTime * m_Speed);
        }

        //transform.position += new Vector3(Input.GetAxis("Horizontal") * m_Speed, 0, Input.GetAxis("Vertical") * m_Speed);
        ConsomeOxygen();
    }

    private void ConsomeOxygen()
    {
        m_CurrentDepth = transform.position.z;

        if (m_CurrentDepth > m_MaxDepth)
        {
            if (m_OxygenConsumeTimer <= 0f)
            {
                m_Oxygen -= 6;
                m_OxygenConsumeTimer = 3f;
            }
        }
        else
        {
            if (m_OxygenConsumeTimer <= 0f)
            {
                m_Oxygen -= 3;
                m_OxygenConsumeTimer = 3f;
            }
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

    public void UpgradeMaxDepth(int multiplier)
    {
        m_MaxDepth = 10f * multiplier;
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

        Physics.Raycast(ray, out hit, Mathf.Infinity);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 50, Color.red);

        Quaternion rotation = Quaternion.LookRotation(hit.point);
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
