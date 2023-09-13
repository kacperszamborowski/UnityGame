using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpawners : MonoBehaviour
{
    public GameObject[] spawners;

    private void OnEnable()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SetActive(true);
        }
    }
    private void OnDisable()
    {
        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SetActive(false);
        }
    }
}
