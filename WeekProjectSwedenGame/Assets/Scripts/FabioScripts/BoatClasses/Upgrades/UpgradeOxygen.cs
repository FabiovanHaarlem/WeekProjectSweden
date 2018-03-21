using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOxygen : MonoBehaviour
{
    [SerializeField]
    private Player m_Player;
    [SerializeField]
    private GameObject m_BuyIcon;


    private int m_UpgradeMultiplier;

    private void Start()
    {
        m_UpgradeMultiplier = 1;
        m_BuyIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_BuyIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_BuyIcon.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_Player.RemoveGold(15 * m_UpgradeMultiplier))
            {
                Debug.Log("Upgarde Oxygen");
                m_Player.UpgradeAirTank(m_UpgradeMultiplier);
                m_UpgradeMultiplier += 1;
            }
        }
    }
}
