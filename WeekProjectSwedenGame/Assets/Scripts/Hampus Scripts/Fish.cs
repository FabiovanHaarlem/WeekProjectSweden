using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour {

    private NavMeshAgent m_navMeshAgent;

    private Vector3 m_TargetPosition;

	// Use this for initialization
	void Awake () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        //m_navMeshAgent.SetDestination(new Vector3());
    }
    public void SpawnFish(Vector3 position,Vector3 TargetPosition)
    {
        this.gameObject.SetActive(true);
        transform.position = position;
        m_navMeshAgent.SetDestination(TargetPosition);
        m_TargetPosition = TargetPosition;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Vector3.Distance(transform.position, m_TargetPosition) < 0.6f)
        {
            this.gameObject.SetActive(false);
        }
	}
}
