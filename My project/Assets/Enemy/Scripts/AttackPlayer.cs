using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private float attackTimer = 0;
    [SerializeField] private float timeToAttack = 1;

    public bool attackFlag;
    GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            Debug.Log("About to attack player");
            attackFlag = true;
            player = other.gameObject;

        }


    }


    private void OnTriggerExit(Collider other)
    {
        attackFlag = false;
        attackTimer = 0;
        player = null;
    }

    private void Start()
    {

    }


    private void Update()
    {
        if (attackFlag)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= timeToAttack)
            {
                Debug.Log("ATTACKIIIING");
                MeleeAttack();
                attackTimer = 0;
            }
        }


    }
    private void MeleeAttack()
    {
        if (player == null) return;

        PlayerLogic playerLogic = player.GetComponentInParent<PlayerLogic>();

        if (playerLogic == null) return;

        playerLogic.TakeDamage();
    }





}
