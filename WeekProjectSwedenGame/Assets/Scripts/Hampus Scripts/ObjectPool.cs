using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    private List<Fish> fishlist;

	// Use this for initialization
	void Start () {
        fishlist = new List<Fish>();
        for (int i = 0; i < 1; i++)
        {
            GameObject gameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Fish"));
            fishlist.Add(gameObject.GetComponent<Fish>());
            gameObject.SetActive(false);
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
