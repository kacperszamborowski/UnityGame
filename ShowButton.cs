using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour
{
    public GameObject e_button;
    public PlayerController player;
    public GameObject dialogueBox;
    public DialogueScript dialogue;

    bool inRange;

    private void Start()
    {
        inRange = false;
    }

    private void Update()
    {
        if (inRange)
        {
            if (!DialogueScript.isTalking)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    e_button.SetActive(false);
                    dialogueBox.SetActive(true);
                    dialogue.StartDialogue();
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (DetectionZone.chasing == false)
            {
                e_button.SetActive(true);
                inRange = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            e_button.SetActive(false);
            inRange = false;
        }
    }
}
