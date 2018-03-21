using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfWater : MonoBehaviour
{
    [SerializeField]
    private Player m_Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Player.ResetOxygen();
        }
    }

}
