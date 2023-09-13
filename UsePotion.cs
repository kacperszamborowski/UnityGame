using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsePotion : MonoBehaviour
{
    public Slider potionBar;
    public PlayerHP playerHP;
    public HealthBar healthBar;
    public TextMeshProUGUI displayPotion;

    public float maxCD;
    private bool isCooldown;

    public int potionAmount;

    private void Start()
    {
        potionBar.maxValue = maxCD;
        potionBar.value = 0;
    }
    void Update()
    {
        PotionUse();
        displayPotion.text = "" + potionAmount;
    }

    void PotionUse()
    {
        if(Input.GetKeyDown(KeyCode.Q) && isCooldown == false)
        {
            if (potionAmount > 0)
            {
                potionAmount--;
                isCooldown = true;
                potionBar.value = 5;
                playerHP._healthP += 30;
                if(playerHP._healthP > playerHP.maxHealth)
                {
                    playerHP._healthP = playerHP.maxHealth;
                }
                healthBar.SetHealth(playerHP._healthP);

            }
        }

        if(isCooldown)
        {
            potionBar.value -= 1 / 1 * Time.deltaTime;

            if(potionBar.value <= 0)
            {
                potionBar.value = 0;
                isCooldown = false;
            }
        }
    }

}
