using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOxygen : MonoBehaviour
{
    [SerializeField]
    private Player m_Player;

    private int m_UpgradeMultiplier;

    private void Start()
    {
        m_UpgradeMultiplier = 1;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_Player.RemoveGold(15 * m_UpgradeMultiplier))
            {
                m_Player.UpgradeAirTank(m_UpgradeMultiplier);
                m_UpgradeMultiplier += 1;
            }
        }
    }
}
