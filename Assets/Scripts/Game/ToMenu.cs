using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
    public GameObject GoPanel;
    private AudioSource buttonSound;

    public void Start()
    {
        buttonSound = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
    }

    public void Pl()
    {
        buttonSound.Play();
    }

    public void ToMe()
    {
        SceneManager.LoadScene(0);
    }

    public void ToShop()
    {
        SceneManager.LoadScene(21);
    }

    public void ToNex()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*public void ToGo()
    {
        Time.timeScale = 1;
        GoPanel.SetActive(false);
    }*/

    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
        // Or / And
        AudioListener.volume = silence ? 0 : 1;
    }


}
