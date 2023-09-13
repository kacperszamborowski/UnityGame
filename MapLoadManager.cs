using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoadManager : MonoBehaviour
{
    public int mapIndex = 0;
    public GameObject[] map = new GameObject[10];
    public SavedVariables load;

    private void Start()
    {
        /*if (MainMenu.firstPlay == true)
        {
            map[0].SetActive(true);
        }
        else if(DeathMenu.died)
        {
            load.Load();
        }
        else
        {
            load.Load();
        }*/
        load.Load();
    }
    public void LoadMap(int mapIndex)
    {
        map[0].SetActive(false);
        map[mapIndex].SetActive(true);
    }
}
