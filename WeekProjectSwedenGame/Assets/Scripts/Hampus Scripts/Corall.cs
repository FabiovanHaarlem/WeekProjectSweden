using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corall : MonoBehaviour {

    [SerializeField]
    private List<GameObject> RemainingTrash;

	// Use this for initialization
	void Start () {
		
	}

    public void RemoveFromList(GameObject Trash)
    {
        for (int i = 0; i < RemainingTrash.Count; i++)
        {
            if (RemainingTrash[i].name == Trash.name)
            {
                RemainingTrash.RemoveAt(i);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (RemainingTrash.Count == 0)
        {
            //Play animation here :)
        }
	}
}
