using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public PlayerController player;
    public QuestScript questScript;
    public ZabkaMenu zabkaMenu;

    public TextMeshProUGUI textComponent;
    public string[] beginLines;
    public string[] activeLines;
    public string[] completeLines;
    public string[] currentLines;
    public float textSpeed;
    public static bool isTalking = false;
    public bool questStart;
    public static bool questCompleted;
    public int questID;

    private int index;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == currentLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = currentLines[index];
            }
        }
    }

    public void StartDialogue()
    {
        isTalking = true;
        textComponent.text = string.Empty;
        player.LockMovement();
        player.LockAttack();
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (this.transform.parent.parent.tag != "Shopkeeper")
        {
            if (questScript.sideQuestID != this.questID && questScript.rewardsGranted[this.questID] == false)
            {
                currentLines = beginLines;
                questScript.sideQuestProgress = 0;
                questStart = true;
                questCompleted = false;
            }
            else if (questScript.sideQuestID == this.questID && !questCompleted)
            {
                currentLines = activeLines;
            }
            else if (questScript.sideQuestID == this.questID && questCompleted)
            {
                currentLines = completeLines;
            }
            else if (questScript.sideQuestID == this.questID && questScript.rewardsGranted[this.questID] == false)
            {
                currentLines = activeLines;
            }
            else if(questScript.rewardsGranted[this.questID] == true)
            {
                currentLines = new string[1];
                currentLines[0] = completeLines[0];
            }
        }
        else
        {
            questStart = false;
            activeLines = beginLines;
            completeLines = beginLines;
            currentLines = beginLines;
        }
        foreach (char c in currentLines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < currentLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            player.UnlockMovement();
            player.UnlockAttack();
            if (this.transform.parent.parent.tag != "Shopkeeper")
            {
                if (questStart)
                {
                    questScript.sideQuestID = this.questID;
                }
                if (questCompleted)
                {
                    questScript.GrantRewards(this.questID);
                    questCompleted = false;
                }
                questStart = false;
            }
            else
            {
                zabkaMenu.OpenShop();
            }
            isTalking = false;
        }
    }
}