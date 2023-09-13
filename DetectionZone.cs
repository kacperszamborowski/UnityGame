using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public Collider2D col;
    public bool isChasing = false;
    public static bool chasing = false;
    public string chaseTag = "Player";
    public CircleCollider2D zone;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == chaseTag)
        {
            isChasing = true;
            chasing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isChasing = false;
        chasing = false;
    }
}

