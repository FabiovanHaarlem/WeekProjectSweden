using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RotationSpeed;
    [SerializeField]
    private float m_Oxygen;
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

    [SerializeField]
    private Image m_CurrentOxygenImage;
    [SerializeField]
    private Image m_CurrentWeightImage;

    [SerializeField]
    private Color m_StartingColor;
    [SerializeField]
    private Color m_EndingColor;

    [SerializeField]
    private Text m_MoneyText;

    [SerializeField]
    private AudioSource m_PickUpSound;
    [SerializeField]
    private Spawner m_Spawner;

    private float m_MaxOxygen;
    private int m_Money;
    private float m_MaxWeight;
    private float m_CurrentWeight;

    private int m_State;
    private float m_TrashCollected;

    void Start()
    {
        RenderSettings.fogColor = m_StartingColor;
        m_CollectedTrash = new List<GameObject>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 2.5f;
        m_RotationSpeed = 50f;
        m_Oxygen = 30f;
        m_MaxOxygen = 30f;
        m_MaxWeight = 20f;
        m_OxygenConsumeTimer = 3f;
        m_Money = 0;
        m_TrashCollected = 0f;
        m_MoneyText.text = "$" + m_Money;
        UpdateWeightIcon();
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R");
            m_TrashCollected += 0.5f;
            ChangeFogColor();
        }

        //transform.position += new Vector3(Input.GetAxis("Horizontal") * m_Speed, 0, Input.GetAxis("Vertical") * m_Speed);
        if (m_Oxygen <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }

        ConsomeOxygen();
        SetCamera();
        m_Animator.SetInteger("State", m_State);
    }

    public void AddGold(int amount)
    {
        m_Money += amount;
        m_MoneyText.text = "$" + m_Money;
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
        m_OxygenConsumeTimer -= Time.deltaTime;

        if (m_OxygenConsumeTimer <= 0f)
        {
            m_Oxygen -= 3;
            m_OxygenConsumeTimer = 3f;
            UpdateAirTank();
        }
    }

    public List<GameObject> SellTrash()
    {
        List<GameObject> trash = new List<GameObject>(m_CollectedTrash);
        m_CollectedTrash.Clear();
        UpdateWeightIcon();
        m_CurrentWeight = 0f;

        return trash;
    }

    private void ChangeFogColor()
    {
        RenderSettings.fogColor = Color.Lerp(m_StartingColor, m_EndingColor, m_TrashCollected / 10);
    }

    private void UpdateAirTank()
    {
        m_CurrentOxygenImage.rectTransform.localScale = new Vector3(1, (m_Oxygen / m_MaxOxygen), 1);
    }

    private void UpdateWeightIcon()
    {
        Color greenColor = new Color(0, 140, 0, 0.25f);
        Color orageColor = new Color(140, 140, 0, 0.25f);
        Color redColor = new Color(140, 0, 0, 0.25f);
        Color currentColor = greenColor;
        m_CurrentWeightImage.rectTransform.localScale = new Vector3(1, (m_CurrentWeight / m_MaxWeight), 1);

        if (m_CurrentWeight > (m_MaxWeight * 0.8))
        {
            currentColor = redColor;
        }
        else if (m_CurrentWeight >= (m_MaxWeight * 0.5))
        {
            currentColor = orageColor;
        }
        else if (m_CurrentWeight < (m_MaxWeight * 0.5))
        {
            currentColor = greenColor;
        }
        m_CurrentWeightImage.color = currentColor;
    }

    public void UpgradeAirTank(int amount)
    {
        m_MaxOxygen += amount;
        ResetOxygen();
    }

    public void UpgradeWeightLimit(int amount)
    {
        m_MaxWeight += amount;
    }

    public void UpgradeSpeed(float amount)
    {
        m_Speed += amount;
        m_RotationSpeed += amount;
    }

    public void ResetOxygen()
    {
        m_Oxygen = m_MaxOxygen;
        UpdateAirTank();
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
        if (collidedObject.CompareTag("TrashBag") || collidedObject.CompareTag("Barrel" )|| 
            collidedObject.CompareTag("Can") || collidedObject.CompareTag("Plastic") || collidedObject.CompareTag("Tire"))
        {
            int weight = 0;

            switch (collidedObject.tag)
            {
                case "TrashBag":
                    weight = 15;
                    break;
                case "Barrel":
                    weight = 25;
                    break;
                case "Can":
                    weight = 5;
                    break;
                case "Plastic":
                    weight = 5;
                    break;
                case "Tire":
                    weight = 25;
                    break;
            }

            if (m_CurrentWeight + weight < m_MaxWeight)
            {
                m_CollectedTrash.Add(collidedObject.gameObject);
                collidedObject.gameObject.SetActive(false);

                switch (collidedObject.tag)
                {
                    case "TrashBag":
                        m_CurrentWeight += 15;
                        break;
                    case "Barrel":
                        m_CurrentWeight += 25;
                        break;
                    case "Can":
                        m_CurrentWeight += 5;
                        break;
                    case "Plastic":
                        m_CurrentWeight += 5;
                        break;
                    case "Tire":
                        m_CurrentWeight += 25;
                        break;
                }
                UpdateWeightIcon();
                m_TrashCollected += 0.5f;
                ChangeFogColor();
                m_PickUpSound.Play();
                m_Spawner.SpawnMoreFish();
            }
        }
    }
}
