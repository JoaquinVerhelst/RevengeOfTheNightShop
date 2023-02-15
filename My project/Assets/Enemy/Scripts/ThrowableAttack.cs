using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAttack : MonoBehaviour
{

    EnemyBehaviour enemyBehaviour;


    private bool doOnce = true;

    private void Start()
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }


    public void SpawnAndHoldBottle()
    {
        enemyBehaviour.SpawnAndHoldBottle();
    }

    public void SetObjectIsThrown()
    {
        enemyBehaviour.objectIsThrown = true;
    }



}
