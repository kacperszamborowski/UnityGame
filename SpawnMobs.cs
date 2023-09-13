using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobs : MonoBehaviour
{
    public GameObject enemy;
    public QuestScript questScript;

    public int minLevel;
    public int maxLevel;
    public int enemyLimit;
    public Vector2 spawnRange;

    private void OnEnable()
    {
        for (int i = 0; i < enemyLimit; i++)
        {
            spawnRange.x = Random.Range(this.transform.position.x-1, this.transform.position.x+1);
            spawnRange.y = Random.Range(this.transform.position.y-1, this.transform.position.y+1);
            GameObject obj = Instantiate(enemy, spawnRange, Quaternion.identity) as GameObject;
            obj.transform.parent = gameObject.transform;
            DamagableEnemy level = obj.GetComponentInChildren<DamagableEnemy>();
            level.level = Random.Range(minLevel, maxLevel);
        }
    }
    private void OnDisable()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
