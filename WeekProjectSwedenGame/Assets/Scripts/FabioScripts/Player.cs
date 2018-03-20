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

    private List<GameObject> m_CollectedTrash;

    void Start()
    {
        m_CollectedTrash = new List<GameObject>();
        m_Speed = 0.6f;
        m_RotationSpeed = 50f;
        m_Oxygen = 100;
        m_OxygenConsumeTimer = 3f;
    }

	void Update ()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * m_Speed, 0, Input.GetAxis("Vertical") * m_Speed);
        ConsomeOxygen();
    }

    private void ConsomeOxygen()
    {
        if (m_OxygenConsumeTimer <= 0f)
        {
            m_Oxygen -= 3;
        }
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
            collidedObject.gameObject.SetActive(false);
        }
    }
}
