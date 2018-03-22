using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private AudioSource m_AudioSource;

    public void StartGame()
    {
        m_AudioSource.Play();
        SceneManager.LoadScene("Fabio");
    }

    public void GoToCredits()
    {
        m_AudioSource.Play();
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        m_AudioSource.Play();
        Application.Quit();
    }
	

}
