using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBoat : MonoBehaviour
{
    [SerializeField]
    private Player m_Player;

    private List<GameObject> m_CollectedTrash;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            List<GameObject> trash = m_Player.SellTrash();

            if (trash.Count >= 1)
            {

                int totalGoldEarned = 0;

                for (int i = 0; i < trash.Count; i++)
                {
                    int gold = FindRightPrice(trash[i].tag);
                    totalGoldEarned = gold;
                }

                m_Player.AddGold(totalGoldEarned);
                trash.Clear();
            }
        }
    }

    private int FindRightPrice(string tagTrash)
    {
        int gold = 0;

        switch(tagTrash)
        {
            case "TrashBar":
                gold = 7;
                break;
            case "Barrel":
                gold = 15;
                break;
            case "Can":
                gold = 3;
                break;
            case "Plastic":
                gold = 4;
                break;
            case "Tire":
                gold = 6;
                break;
        }

        return gold;
    }
}
