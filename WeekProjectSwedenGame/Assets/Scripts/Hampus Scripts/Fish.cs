using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour {

    private NavMeshAgent m_navMeshAgent;

	// Use this for initialization
	void Start () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.SetDestination(new Vector3());
    }
    public void SpawnFish(Vector3 position,Vector3 TargetPosition)
    {
        transform.position = position;
        m_navMeshAgent.SetDestination(TargetPosition);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
