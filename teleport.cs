using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameObject exit;
    public Animator transition;
    public SavedVariables save;
    public PlayerController player;
    public GameObject questList;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            player.LockMovement();
            player.LockAttack();
            PlayerController.dashing = false;
            Physics2D.IgnoreLayerCollision(player.playerLayer, player.enemyLayer, false);
            transition.SetTrigger("fade_in");
            questList.SetActive(false);
            StartCoroutine(tp(col));
            StartCoroutine(fade());
        }
    }

    IEnumerator tp(Collider2D col)
    {
        yield return new WaitForSeconds(1f);

        col.transform.position = exit.transform.position;
        if (GameObject.FindGameObjectWithTag("Follower") != null)
            GameObject.FindGameObjectWithTag("Follower").transform.position = exit.transform.position;
        save.Save();
    }
    IEnumerator fade()
    {
        yield return new WaitForSeconds(2f);

        transition.SetTrigger("fade_out");
        questList.SetActive(true);
        player.UnlockMovement();
        player.UnlockAttack();
    }
}
