using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public static bool firstPlay = true;
    public GameObject warning;
    public GameObject menu;
    private void Start()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
            firstPlay = false;
    }
    public void PlayGame()
    {
        if(firstPlay == true)
            SceneManager.LoadScene("SampleScene");
        else
        {
            menu.SetActive(false);
            warning.SetActive(true);
        }

    }

    public void ContinueGame()
    {
        if (firstPlay == false)
            SceneManager.LoadScene("SampleScene");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DeleteSave()
    {
        File.Delete(Application.dataPath + "/save.txt");
        firstPlay = true;
        SceneManager.LoadScene("SampleScene");
    }

    public void DoNotDelete()
    {
        menu.SetActive(true);
        warning.SetActive(false);
    }
}
