using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "car")
        {
            Destroy(collision.gameObject);
        }
    }
}
