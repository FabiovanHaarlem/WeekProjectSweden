using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    private List<Fish> fishlist;

	// Use this for initialization
	void Start () {
        fishlist = new List<Fish>();
        for (int i = 0; i < 15; i++)
        {
            GameObject fish0 = Instantiate(Resources.Load<GameObject>("Prefabs/Fish01"));
            fishlist.Add(fish0.GetComponent<Fish>());
            fish0.SetActive(false);
            GameObject fish1 = Instantiate(Resources.Load<GameObject>("Prefabs/Fish02"));
            fishlist.Add(fish1.GetComponent<Fish>());
            fish1.SetActive(false);
            GameObject fish2 = Instantiate(Resources.Load<GameObject>("Prefabs/Fish03"));
            fishlist.Add(fish2.GetComponent<Fish>());
            fish2.SetActive(false);
            GameObject fish3 = Instantiate(Resources.Load<GameObject>("Prefabs/Fish04"));
            fishlist.Add(fish3.GetComponent<Fish>());
            fish3.SetActive(false);
            GameObject fish4 = Instantiate(Resources.Load<GameObject>("Prefabs/Fish05"));
            fishlist.Add(fish4.GetComponent<Fish>());
            fish4.SetActive(false);
            GameObject fish5 = Instantiate(Resources.Load<GameObject>("Prefabs/Fish06"));
            fishlist.Add(fish5.GetComponent<Fish>());
            fish5.SetActive(false);

        }
	}
    public void GetFish(Vector3 position, Vector3 TargetPosition)
    {
        for (int i = 0; i < fishlist.Count; i++)
        {
            if (fishlist[i].gameObject.activeInHierarchy != true)
            {
                fishlist[i].SpawnFish(position, TargetPosition);
                break;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
