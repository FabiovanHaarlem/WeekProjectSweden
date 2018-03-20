using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    [SerializeField]
    private Corall Corall;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Corall.RemoveFromList(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
