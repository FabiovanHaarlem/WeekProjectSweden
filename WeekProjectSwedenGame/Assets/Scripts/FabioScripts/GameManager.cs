using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;
	
	void Start ()
    {
        //m_AudioSource.Play();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Fabio");
        }
    }
}
