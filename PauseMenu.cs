using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject followersMenu;
    public GameObject questList;
    public SavedVariables save;
    public static bool isPaused;

    private void Start()
    {
        save = GetComponent<SavedVariables>();
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (ZabkaMenu.isShopping == false)
            {
                if (isPaused)
                {
                    ResumeGame();
                    followersMenu.SetActive(false);
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        questList.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        questList.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitAndSave()
    {
        save.Save();
        pauseMenu.SetActive(false);
        questList.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        pauseMenu.SetActive(false);
        questList.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Followers()
    {
        pauseMenu.SetActive(false);
        followersMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
}
