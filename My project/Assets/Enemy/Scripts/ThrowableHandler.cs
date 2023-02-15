using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableHandler : MonoBehaviour
{


    private float toThrowTimer;

    public float throwAnimTime = 0;
    public bool isThrowAnimStarted = false;
    EnemyBehaviour enemyBehaviour;
    Animator animator;
    public bool objectIsThrown = false;

    private void Start()
    {

        enemyBehaviour = GetComponent<EnemyBehaviour>();
        animator = GetComponentInChildren<Animator>();
    }

    public void UpdateThrowing(GameObject cloneThrowable, Transform targetTransform)
    {

         ThrowObjectLogic(cloneThrowable, targetTransform);
        

        if (objectIsThrown && cloneThrowable != null)
        {
            Throw(cloneThrowable, targetTransform);
        }
    }



    private void Throw(GameObject cloneThrowable, Transform targetTransform)
    {
        objectIsThrown = false;

        Vector3 direction = transform.position - targetTransform.position;
        direction.Normalize();

        Vector3 throwableTarget = targetTransform.position - direction * 1.5f;


        cloneThrowable.GetComponent<ThrowableObjectCollision>().Initialize(throwableTarget, enemyBehaviour.firingAngle);
        cloneThrowable.GetComponent<ThrowableObjectCollision>().isThrown = true;
    }




    public void ThrowObjectLogic(GameObject cloneThrowable, Transform targetTransform)
    {
        toThrowTimer += Time.deltaTime;

        // Initializing Anim lenght
        if (throwAnimTime == 0)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;

            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == "Throw")
                {
                    throwAnimTime = ac.animationClips[i].length;
                }
            }
        }


        if (toThrowTimer >= enemyBehaviour.timerToThrow && cloneThrowable == null)
        {
            isThrowAnimStarted = true;
            animator.CrossFade("Throw", 0.2f);
            toThrowTimer = 0;
        }

    }
}
