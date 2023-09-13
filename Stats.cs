using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int level = 1;
    public int expo = 0;
    public int expoNaLvl = 100;
    public int currentHealth = 100;
    public int maxHealth = 100;
    public int mana = 20;
    public int maxMana = 20;

    public HealthBar healthBar;
    public ExpBar expBar;
    public PlayerHP hp;

    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        hp = GetComponent<PlayerHP>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

        //get mana
        //manaBar = GetComponentInChildren<ManaBar>();
        //wczytaj staty z get mana

        //get exp
        expBar = GetComponentInChildren<ExpBar>();
        expBar.SetExpForLevel(expoNaLvl);
        expBar.SetExp(expo);

    }
    private void FixedUpdate()
    {
        currentHealth = hp._healthP;
        maxHealth = hp.maxHealth;
        //mana = mp._manaP;
        //maxMana = mp.maxMana;
        expBar.SetExp(expo);
        if (expo >= expoNaLvl)
        {
            level++;
            maxHealth += 15;
            maxMana += 2;
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            mana = maxMana;
            expo = expo - expoNaLvl;
            expoNaLvl += 50;
            expBar.SetExpForLevel(expoNaLvl);
        }
    }


    private void OnApplicationQuit()
    {

    }

}
