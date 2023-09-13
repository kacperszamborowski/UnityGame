using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public int saveFollowerID;
    public GameObject player;
    public GameObject[] follower = new GameObject[5];

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnFollower(int followerID)
    {
        if(followerID == 0)
        {
            return;
        }
        else
        {
            if(GameObject.FindGameObjectWithTag("Follower") == null)
            {
                Instantiate(follower[followerID], player.transform.position, Quaternion.identity);
                saveFollowerID = followerID;
            }
            else if (GameObject.FindGameObjectWithTag("Follower").TryGetComponent<FollowerController>(out FollowerController followerController))
            {
                if (followerController.currentFollowerID != followerID)
                {
                    GameObject go = GameObject.FindGameObjectWithTag("Follower");
                    Destroy(go);
                    Instantiate(follower[followerID], player.transform.position, Quaternion.identity);
                    saveFollowerID = followerID;
                }
            }
        }
    }
}
