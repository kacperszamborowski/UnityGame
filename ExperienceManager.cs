using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    public int level = 1;
    public int expo = 0;
    public int expoNaLvl = 100;

    public PlayerHP playerHp;
    public PlayerController playerMana;
    public HealthBar healthBar;
    public ExpBar expBar;
    public ManaBar manaBar;
    public TextMeshProUGUI displayLevel;
    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        expBar = GetComponentInChildren<ExpBar>();
        expBar.SetExpForLevel(expoNaLvl);
        expBar.SetExp(expo);
        manaBar = GetComponentInChildren<ManaBar>();
        playerHp = GetComponent<PlayerHP>();
        playerMana = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        displayLevel.text = "Level: " + level;
        expBar.SetExp(expo);
        if(expo >= expoNaLvl)
        {
            LevelUp();
        }       
    }

    public void LevelUp()
    {
        level++;
        playerHp.maxHealth += 50;
        playerMana.maxMana += 2;
        playerHp._healthP = playerHp.maxHealth;
        playerMana.mana = playerMana.maxMana;
        expo = expo - expoNaLvl;
        expoNaLvl += 50;
        healthBar.SetMaxHealth(playerHp.maxHealth);
        healthBar.SetHealth(playerHp._healthP);
        manaBar.SetMaxMana(playerMana.maxMana);
        manaBar.SetMana(playerMana.mana);
        expBar.SetExpForLevel(expoNaLvl);
        expBar.SetExp(expo);
        
    }
}
