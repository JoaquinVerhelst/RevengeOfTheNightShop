using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{

    ThrowableHandler throwableHandler;
    EnemyBehaviour enemyBehaviour;

    private bool doOnce = true;

    private void Start()
    {
        throwableHandler = GetComponentInParent<ThrowableHandler>();
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }


    public void SpawnAndHoldBottle()
    {
        enemyBehaviour.SpawnAndHoldBottle();
    }

    public void SetObjectIsThrown()
    {
        throwableHandler.objectIsThrown = true;
    }



}
