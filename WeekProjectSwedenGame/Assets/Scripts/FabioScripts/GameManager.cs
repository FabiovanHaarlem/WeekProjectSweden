using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;
	
	void Start ()
    {
        m_AudioSource.Play();
	}
}
