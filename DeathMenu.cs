using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    public GameObject player;
    public static bool died;

    private void Start()
    {
        deathMenu.SetActive(false);
        died = false;
    }

    public void DeadMenu()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitToMenu()
    {
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}