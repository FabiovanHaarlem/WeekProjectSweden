using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float m_SpawnFishTimer;

    [SerializeField]
    private GameObject m_LeftPoint;
    [SerializeField]
    private GameObject m_RightPoint;

    private ObjectPool m_ObjectPool;

    private void Start()
    {
        m_ObjectPool = GetComponent<ObjectPool>();
        m_SpawnFishTimer = 0.5f;
    }

    private void Update()
    {
        m_SpawnFishTimer -= Time.deltaTime;

        if (m_SpawnFishTimer <= 0f)
        {
            int randomSide = (int)Random.Range(0, 2);
            Vector3 spawnLocation = new Vector3();
            Vector3 targetLocation = new Vector3();

            if (randomSide == 0)
            {
                spawnLocation = new Vector3(m_LeftPoint.transform.position.x, 0, -Random.Range(1, 25));
                targetLocation = new Vector3(m_RightPoint.transform.position.x, 0, -Random.Range(1, 25));
            }
            else if (randomSide == 1)
            {
                spawnLocation = new Vector3(m_RightPoint.transform.position.x, 0, -Random.Range(1, 25));
                targetLocation = new Vector3(m_LeftPoint.transform.position.x, 0, -Random.Range(1, 25));
            }

            m_ObjectPool.GetFish(spawnLocation, targetLocation);
            m_SpawnFishTimer = 0.5f;
        }
    }

}
