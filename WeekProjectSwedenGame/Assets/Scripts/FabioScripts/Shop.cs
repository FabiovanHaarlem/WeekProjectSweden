using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Player m_Player;

    private List<GameObject> m_CollectedTrash;

    private int m_PlayerCash;

    private int m_PlayerAirTankMultiplier;

    private void Start()
    {
        m_PlayerCash = 10;
    }

    public void UpgradeAirTank()
    {
        m_PlayerAirTankMultiplier += 1;
        m_Player.UpgradeAirTank(m_PlayerAirTankMultiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("TrashBoot"))
        {
            if (other.CompareTag("Player"))
            {
                List<GameObject> trash = m_Player.SellTrash();

                for (int i = 0; i < trash.Count; i++)
                {
                    m_PlayerCash += 10;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.CompareTag(""))
        if (other.CompareTag("Player"))
        {
            m_Player.ResetOxygen();
        }
    }
}
