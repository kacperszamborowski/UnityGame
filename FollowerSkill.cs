using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSkill : MonoBehaviour
{
    public CapsuleCollider2D hitbox;
    public Rigidbody2D rb;
    public GameObject[] enemies;
    public DetectionZone[] detectionZone;
    public EnemyController[] enemyController;
    public PlayerController player;
    public static bool canFollowerMove = true;
    public static bool skillActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (player.mana >= 15)
            {
                UseSkill();
                player.mana -= 15;
            }
        }
    }
    void UseSkill()
    {
        StartCoroutine(SkillDuration());
    }

    IEnumerator SkillDuration()
    {
        int i = 0;
        enemies = new GameObject[GameObject.FindGameObjectsWithTag("Entity").Length];
        enemies = GameObject.FindGameObjectsWithTag("Entity");
        detectionZone = new DetectionZone[enemies.Length];
        enemyController = new EnemyController[enemies.Length];
        foreach (GameObject o in enemies)
        {
            detectionZone[i] = enemies[i].GetComponentInChildren<DetectionZone>();
            detectionZone[i].chaseTag = "Follower";
            detectionZone[i].zone.radius = 1.5f;
            enemyController[i] = enemies[i].GetComponent<EnemyController>();
            enemyController[i].chaseTarget = GameObject.FindGameObjectWithTag("Follower");
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            canFollowerMove = false;
            i++;
        }
        player.canRegenMana = false;
        skillActive = true;
        hitbox.enabled = true;

        yield return new WaitForSeconds(5f);

        i = 0;
        foreach (GameObject o in enemies)
        {
            detectionZone[i].chaseTag = "Player";
            if(detectionZone[i] != null)
                detectionZone[i].zone.radius = 0.83f;
            enemyController[i].chaseTarget = GameObject.FindGameObjectWithTag("Player");
            i++;
        }
        enemies = new GameObject[0];
        detectionZone = new DetectionZone[0];
        enemyController = new EnemyController[0];
        rb.rotation = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        canFollowerMove = true;
        player.canRegenMana = true;
        skillActive = false;
    }
}
