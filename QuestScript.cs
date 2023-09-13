using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScript : MonoBehaviour
{
    public int mainQuestID;
    public int mainQuestProgress;
    public int sideQuestID;
    public int sideQuestProgress;
    public bool sideQuestCompleted = false;
    public bool[] rewardsGranted = new bool[15];

    public GameObject player;
    public TextMeshProUGUI mainQuestText;
    public TextMeshProUGUI sideQuestText;


    void Update()
    {
        if (mainQuestID == 1)
        {
            mainQuestText.text = "<u><b>filler</b></u>";
            mainQuestText.fontSize = 40;
        }

        if(sideQuestID == 0)
        {
            sideQuestText.text = string.Empty;
        }

        if (sideQuestID == 1)
        {
            sideQuestText.text = "Przynies Tomeczkowi 12 mikstur: " + sideQuestProgress + "/12";
            sideQuestProgress = GameObject.Find("Player").GetComponent<UsePotion>().potionAmount;
            if(sideQuestProgress >= 12)
            {
                DialogueScript.questCompleted = true;
            }
        }
                  
        if(sideQuestID == 2)
        {
            sideQuestText.text = "Zabij 2 potwory dla Maciusia " + sideQuestProgress + "/2";
            if (sideQuestProgress >= 2)
            {
                DialogueScript.questCompleted = true;
            }
        }
    }

    public void GrantRewards(int questID)
    {
        if (rewardsGranted[questID])
        {
            return;
        }
        else
        {
            if (questID == 1)
            {
                player.GetComponent<UsePotion>().potionAmount -= 12;
                player.GetComponent<ExperienceManager>().expo += 130;
                rewardsGranted[questID] = true;
                sideQuestProgress = 0;
                sideQuestID = 0;
            }
            if(questID == 2)
            {
                player.GetComponent<ExperienceManager>().expo += 250;
                rewardsGranted[questID] = true;
                sideQuestProgress = 0;
                sideQuestID = 0;
            }
        }
    }
}
