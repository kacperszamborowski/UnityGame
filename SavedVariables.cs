using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedVariables : MonoBehaviour
{
    private const string SAVE_SEPARATOR = "\n";

    public GameObject player;
    public PlayerHP playerHp;
    public ExperienceManager exp;
    public MapLoadManager map;
    public HealthBar healthBar;
    public ManaBar manaBar;
    public ExpBar expBar;
    public PlayerController playerMana;
    public UsePotion potions;
    public GoldManager gold;
    public QuestScript quests;
    public FollowerManager followerManager;

    private float posX;
    private float posY;
    private int level;
    private int expo;
    private int expoNaLvl;
    private int currentHealth;
    private int maxHealth;
    private int mana;
    private int maxMana;
    private int mapIndex;
    private int potionAmount;
    private int goldAmount;
    private int sideQuestID;
    private int sideQuestProgress;
    private bool[] rewardsGranted = new bool[15];
    private int followerID;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
    public void Save()
    {
        posX = player.GetComponent<Transform>().transform.position.x;
        posY = player.GetComponent<Transform>().transform.position.y;
        level = exp.level;
        expo = exp.expo;
        expoNaLvl = exp.expoNaLvl;
        currentHealth = playerHp._healthP;
        maxHealth = playerHp.maxHealth;
        mana = playerMana.mana;
        maxMana = playerMana.maxMana;
        mapIndex = map.mapIndex;
        potionAmount = potions.potionAmount;
        goldAmount = gold.goldAmount;
        sideQuestID = quests.sideQuestID;
        sideQuestProgress = quests.sideQuestProgress;
        rewardsGranted = quests.rewardsGranted;
        followerID = followerManager.saveFollowerID;

        string[] contents = new string[]
        {
            ""+posX,
            ""+posY,
            ""+level,
            ""+expo,
            ""+expoNaLvl,
            ""+currentHealth,
            ""+maxHealth,
            ""+mana,
            ""+maxMana,
            ""+mapIndex,
            ""+potionAmount,
            ""+goldAmount,
            ""+sideQuestID,
            ""+sideQuestProgress,
            ""+rewardsGranted[0],
            ""+rewardsGranted[1],
            ""+rewardsGranted[2],
            ""+rewardsGranted[3],
            ""+rewardsGranted[4],
            ""+rewardsGranted[5],
            ""+rewardsGranted[6],
            ""+rewardsGranted[7],
            ""+rewardsGranted[8],
            ""+rewardsGranted[9],
            ""+rewardsGranted[10],
            ""+rewardsGranted[11],
            ""+rewardsGranted[12],
            ""+rewardsGranted[13],
            ""+rewardsGranted[14],
            ""+followerID
        };

        string saveString = string.Join(SAVE_SEPARATOR, contents);
        File.WriteAllText(Application.dataPath + "/save.txt", saveString);
    }

    public void Load()
    {
        if (!MainMenu.firstPlay)
        {
            //Przypisanie wartosci z pliku
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            string[] contents = saveString.Split(SAVE_SEPARATOR, System.StringSplitOptions.None);

            //Pozycja postaci
            posX = float.Parse(contents[0]);
            posY = float.Parse(contents[1]);
            Vector2 pos = new Vector2(posX, posY);

            //Staty
            level = int.Parse(contents[2]);
            expo = int.Parse(contents[3]);
            expoNaLvl = int.Parse(contents[4]);
            currentHealth = int.Parse(contents[5]);
            maxHealth = int.Parse(contents[6]);
            mana = int.Parse(contents[7]);
            maxMana = int.Parse(contents[8]);

            //Mapa
            mapIndex = int.Parse(contents[9]);

            //Potki/Gold
            potionAmount = int.Parse(contents[10]);
            goldAmount = int.Parse(contents[11]);

            //Wczytanie pozycji
            player.transform.position = pos;

            //Questy
            sideQuestID = int.Parse(contents[12]);
            sideQuestProgress = int.Parse(contents[13]);
            rewardsGranted[0] = bool.Parse(contents[14]);
            rewardsGranted[1] = bool.Parse(contents[15]);
            rewardsGranted[2] = bool.Parse(contents[16]);
            rewardsGranted[3] = bool.Parse(contents[17]);
            rewardsGranted[4] = bool.Parse(contents[18]);
            rewardsGranted[5] = bool.Parse(contents[19]);
            rewardsGranted[6] = bool.Parse(contents[20]);
            rewardsGranted[7] = bool.Parse(contents[21]);
            rewardsGranted[8] = bool.Parse(contents[22]);
            rewardsGranted[9] = bool.Parse(contents[23]);
            rewardsGranted[10] = bool.Parse(contents[24]);
            rewardsGranted[11] = bool.Parse(contents[25]);
            rewardsGranted[12] = bool.Parse(contents[26]);
            rewardsGranted[13] = bool.Parse(contents[27]);
            rewardsGranted[14] = bool.Parse(contents[28]);

            //Followersi
            followerID = int.Parse(contents[29]);
        }
        else
        {
            //Przypisanie wartosci bez save

            //Pozycja postaci
            posX = -0.13f; //24f
            posY = 0f; //46f
            Vector2 pos = new Vector2(posX, posY);

            //Staty
            level = 1;
            expo = 0;
            expoNaLvl = 100;
            currentHealth = 100;
            maxHealth = 100;
            mana = 20;
            maxMana = 20;

            //Mapa
            mapIndex = 0;

            //Potki/Gold
            potionAmount = 0;
            goldAmount = 0;

            //Wczytanie pozycji
            player.transform.position = pos;

            //Questy
            sideQuestID = 0;
            sideQuestProgress = 0;
            for (int i = 0; i < rewardsGranted.Length; i++)
                rewardsGranted[i] = false;

            //Followersi
            followerID = 0;
        }
        //Wczytanie wartosci
        exp.level = level;
        exp.expo = expo;
        expBar.SetExp(expo);
        exp.expoNaLvl = expoNaLvl;
        expBar.SetExpForLevel(expoNaLvl);
        playerHp._healthP = currentHealth;
        healthBar.SetHealth(currentHealth);
        playerHp.maxHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerMana.mana = mana;
        manaBar.SetMana(mana);
        playerMana.maxMana = maxMana;
        manaBar.SetMaxMana(maxMana);
        map.mapIndex = mapIndex;
        map.LoadMap(mapIndex);
        potions.potionAmount = potionAmount;
        gold.goldAmount = goldAmount;
        quests.sideQuestID = sideQuestID;
        quests.sideQuestProgress = sideQuestProgress;
        for (int i = 0; i < rewardsGranted.Length; i++)
            quests.rewardsGranted[i] = rewardsGranted[i];
        followerManager.SpawnFollower(followerID);

    }
}
