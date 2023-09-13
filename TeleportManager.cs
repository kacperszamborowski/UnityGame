using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject currentMap;
    public GameObject nextMap;
    //public SavedVariables save;
    public int mapIndex;
    public MapLoadManager loadMap;
    public GameObject[] cars;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            nextMap.SetActive(true);
            mapIndex = nextMap.GetComponent<MapIndex>().mapIndex;
            loadMap.mapIndex = mapIndex;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            currentMap.SetActive(false);
            cars = GameObject.FindGameObjectsWithTag("car");
            for (int i = 0; i < cars.Length; i++)
                Destroy(cars[i]);
        }
    }
}
