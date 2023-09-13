using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public int goldAmount;
    public TextMeshProUGUI displayGold;

    // Update is called once per frame
    void Update()
    {
        displayGold.text = "" + goldAmount;
    }
}
