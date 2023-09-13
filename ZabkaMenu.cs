using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZabkaMenu : MonoBehaviour
{
    public GameObject zabkaMenu;
    public GoldManager gold;
    public UsePotion potki;
    public PlayerController player;

    public TextMeshProUGUI displayGold;

    public static bool isShopping = false;

    private void Start()
    {
        zabkaMenu.SetActive(false);
    }

    public void Update()
    {
        displayGold.text = "PLN: "+gold.goldAmount;
    }

    public void OpenShop()
    {
        zabkaMenu.SetActive(true);
        Time.timeScale = 0f;
        player.LockAttack();
        isShopping = true;
    }
    public void BuyPotion()
    {
        if(gold.goldAmount >= 5)
        {
            gold.goldAmount -= 5;
            potki.potionAmount += 1;
        }
    }
    public void Quit()
    {
        zabkaMenu.SetActive(false);
        Time.timeScale = 1f;
        player.UnlockAttack();
        isShopping = false;
    }
}
