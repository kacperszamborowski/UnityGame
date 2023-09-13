using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float carSpawnTimer;
    public float carSpawnElapsed;
    GameObject obj;

    public bool left;
    public bool right;
    public bool up;
    public bool down;

    public GameObject[] cars = new GameObject[4];

    void Start()
    {
        carSpawnTimer = Random.Range(2f, 8f);
    }

    void Update()
    {
        carSpawnElapsed += Time.deltaTime;
        if(carSpawnElapsed >= carSpawnTimer)
        {
            obj = Instantiate(cars[Random.Range(0,4)], transform.position, Quaternion.identity) as GameObject;
            obj.transform.parent = gameObject.transform;
            if (left)
            {
                obj.GetComponent<carDriveX>().start = new Vector2(this.transform.position.x, this.transform.position.y);
                obj.GetComponent<carDriveX>().end = new Vector2(this.transform.position.x - 20f, this.transform.position.y);
                
            }
            if(right)
            {
                obj.GetComponent<carDriveX>().start = new Vector2(this.transform.position.x, this.transform.position.y);
                obj.GetComponent<carDriveX>().end = new Vector2(this.transform.position.x + 20f, this.transform.position.y);
            }
            if (up)
            {
                obj.GetComponent<carDriveX>().start = new Vector2(this.transform.position.x, this.transform.position.y);
                obj.GetComponent<carDriveX>().end = new Vector2(this.transform.position.x, this.transform.position.y + 20f);
            }
            if (down)
            {
                obj.GetComponent<carDriveX>().start = new Vector2(this.transform.position.x, this.transform.position.y);
                obj.GetComponent<carDriveX>().end = new Vector2(this.transform.position.x, this.transform.position.y - 20f);
            }
            carSpawnElapsed = 0f;
            carSpawnTimer = Random.Range(2f, 8f);
        }
    }
}
