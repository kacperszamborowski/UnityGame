using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMenu : MonoBehaviour
{
    public FollowerManager followerManager;
    public GameObject followerMenu;
    public GameObject pauseMenu;
    public GameObject questList;

    public void Follower1()
    {
        followerMenu.SetActive(false);
        questList.SetActive(true);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        followerManager.SpawnFollower(1);
    }

    public void Follower2()
    {
        followerMenu.SetActive(false);
        questList.SetActive(true);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        followerManager.SpawnFollower(2);
    }

    public void GoBack()
    {
        followerMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
    }
}
