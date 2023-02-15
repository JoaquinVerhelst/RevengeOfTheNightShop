using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class EnemyBehaviour : MonoBehaviour
{
    LayerMask worldLayer;
    NavMeshAgent agent;
    public Transform targetTransform;
    //public bool objectIsThrown = false;
    Animator animator;
    ThrowableHandler throwableHandler;
    WanderHandler wanderHandler;


    [HideInInspector]
    public GameObject cloneThrowable;


    [SerializeField] public GameObject throwableObject;
    [SerializeField] public Transform throwablePlacement;
    [SerializeField] public float timerToThrow = 3f;
    [SerializeField] public float minDistanceToThrow = 15f;
    [SerializeField] public float maxDistanceToThrow = 30f;
    [SerializeField] public float firingAngle = 45f;


    private float walkResetTimer = 0;
    public bool isWalking = true;


    private float playerOutOfPOVTimer = 0;
    [SerializeField] private float timerToWander;
    private bool isAgro = true;
    private bool isOutOfSight;


// Start is called before the first frame update
void Start()
    {
        worldLayer = LayerMask.GetMask("World");
        agent = GetComponent<NavMeshAgent>();
        throwableHandler = GetComponent<ThrowableHandler>();
        animator = GetComponentInChildren<Animator>();
        wanderHandler = GetComponent<WanderHandler>();
    }


    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }



    private void Update()
    {



        if (isAgro)
        {
            if (CheckIfTargetIsInPOV(targetTransform))
            {
                playerOutOfPOVTimer = 0;
                isAgro = true;
                isOutOfSight = false;

                throwableHandler.UpdateThrowing(cloneThrowable, targetTransform);
            }
            else
            {
                isOutOfSight = true;
            }
            if (isWalking)
            {
                agent.isStopped = false;
                MoveTowardsPlayer();
            }

        }
        if (isOutOfSight)
        {
            playerOutOfPOVTimer += Time.deltaTime;

            if (playerOutOfPOVTimer >= timerToWander)
            {
                isAgro = false;
                wanderHandler.UpdateWander();
            }
        }
        if(!isAgro && CheckIfTargetIsInPOV(targetTransform))
        {
            isAgro = true;

        }
        if (throwableHandler.isThrowAnimStarted)
        {
            isWalking = false;
            agent.isStopped = true;
            walkResetTimer += Time.deltaTime;

            if (walkResetTimer >= throwableHandler.throwAnimTime)
            {

                walkResetTimer = 0;
                animator.SetBool("IsObjectThrown", true);
                throwableHandler.isThrowAnimStarted = false;
                isWalking = true;
            }
        }


    }

    public void MoveTowardsPlayer()
    {
        if (targetTransform != null)
        {
            Vector3 target = targetTransform.transform.position;
            agent.SetDestination(target);
            animator.SetBool("IsWalking", true);
        }
    }

    public void SpawnAndHoldBottle()
    {
        cloneThrowable = Instantiate(throwableObject, throwablePlacement);
    }



    public bool CheckIfTargetIsInPOV(Transform targetTransform)
    {
        Vector3 directionToTarget = targetTransform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        float distance = directionToTarget.magnitude;

        if (Mathf.Abs(angle) < 180 && distance > minDistanceToThrow && distance < maxDistanceToThrow)
        {
            RaycastHit hit;

            if (Physics.Linecast(transform.position, targetTransform.position, out hit))
            {
                if (hit.transform.gameObject.layer == worldLayer)
                    return false;
                else
                    return true;
            }
        }

        return false;
    }





}






