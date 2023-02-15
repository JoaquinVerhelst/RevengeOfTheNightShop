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

    NavMeshAgent agent;
    public Transform targetTransform;
    LayerMask worldLayer;
    private float toThrowTimer;
    Animator animator;

    [HideInInspector]
    public GameObject cloneThrowable;


    [SerializeField] public GameObject throwableObject;
    [SerializeField] public Transform throwablePlacement;
    [SerializeField] private float timerToThrow = 3f;
    [SerializeField] private float minDistanceToThrow = 15f;
    [SerializeField] private float maxDistanceToThrow = 30f;
    [SerializeField] public float firingAngle = 45f;
    [SerializeField] public bool canThrow = true;

    private float throwAnimTime = 0;
    private bool isThrowAnimStarted = false;
    private float walkResetTimer = 0;
    private bool isWalking = true;

    public bool objectIsThrown = false;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        worldLayer = LayerMask.GetMask("World");
        animator = GetComponentInChildren<Animator>();
    }


    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }



    private void Update()
    {


        if (canThrow)
        {
            ThrowObjectLogic();
        }



        if (objectIsThrown && cloneThrowable != null)
        {
            objectIsThrown = false;

            Vector3 direction = transform.position - targetTransform.position;
            direction.Normalize();

            Vector3 throwableTarget = targetTransform.position - direction * 2.5f;


            cloneThrowable.GetComponent<ThrowableObjectCollision>().Initialize(throwableTarget, firingAngle);
            cloneThrowable.GetComponent<ThrowableObjectCollision>().isThrown = true;
        }

        if (isThrowAnimStarted)
        {
            isWalking = false;
            agent.isStopped = true;
            walkResetTimer += Time.deltaTime;

            if (walkResetTimer >= throwAnimTime)
            {

                walkResetTimer = 0;
                animator.SetBool("IsObjectThrown", true);
                isThrowAnimStarted = false;
                isWalking = true;
            }
        } 
        else if (isWalking)
        {
            agent.isStopped = false;
            MoveTowardsPlayer();
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

    private bool CheckToThrow()
    {
        Vector3 directionToTarget = targetTransform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        float distance = directionToTarget.magnitude;

        if (Mathf.Abs(angle) < 60 && distance > minDistanceToThrow && distance < maxDistanceToThrow)
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

    public void ThrowObjectLogic()
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


        if (CheckToThrow() && toThrowTimer >= timerToThrow && cloneThrowable == null)
        {
            isThrowAnimStarted = true;
            animator.CrossFade("Throw", 0.2f);
            toThrowTimer = 0;
        }

    }

    public void SpawnAndHoldBottle()
    {
        cloneThrowable = Instantiate(throwableObject, throwablePlacement);
    }




    //IEnumerable SimulateProjectile()
    //{

    //    // Move projectile to the position of throwing object + add some offset if needed.
    //    //throwableObject.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

    //    //if (cloneThrowable == null)
    //    //{
    //    //    isNotThrown = true;
    //    //    return;
    //    //}

    //    //if (isNotThrown)
    //    //{
    //    //    Vector3 direction = transform.position - targetTransform.position;
    //    //    direction.Normalize();

    //    //    throwableTarget = targetTransform.position - direction * 2.5f;
    //    //    isNotThrown = false;

    //    //}


    //    // Calculate distance to target
    //    float target_Distance = Vector3.Distance(cloneThrowable.transform.position, throwableTarget);

    //    // Calculate the velocity needed to throw the object to the target at specified angle.
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //    // Extract the X  Y componenent of the velocity
    //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //    // Calculate flight time.
    //    float flightDuration = target_Distance / Vx;

    //    // Rotate projectile to face the target.
    //    cloneThrowable.transform.rotation = Quaternion.LookRotation(throwableTarget - cloneThrowable.transform.position);

    //    float elapse_time = 0;


    //    while (elapse_time < flightDuration)
    //    {
    //        //if (cloneThrowable == null)
    //        //{
    //        //    isNotThrown = true;
    //        //    return;
    //        //}

    //        cloneThrowable.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

    //        elapse_time += Time.deltaTime;

    //        return;
    //    }
    //}
    //IEnumerator SimulateProjectile()
    //{


    //    // Move projectile to the position of throwing object + add some offset if needed.
    //    //throwableObject.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

    //    if (cloneThrowable == null)
    //    {
    //        isNotThrown = true;
    //        yield break;
    //    }

    //    if (isNotThrown)
    //    {
    //        Vector3 direction = transform.position - targetTransform.position;
    //        direction.Normalize();

    //        throwableTarget = targetTransform.position - direction * 2.5f;
    //        isNotThrown = false;
    //    }

    //    // Calculate distance to target
    //    float target_Distance = Vector3.Distance(cloneThrowable.transform.position, throwableTarget);

    //    // Calculate the velocity needed to throw the object to the target at specified angle.
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity) * Time.deltaTime;

    //    // Extract the X  Y componenent of the velocity
    //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //    // Calculate flight time.
    //    float flightDuration = (target_Distance / Vx);

    //    // Rotate projectile to face the target.
    //    cloneThrowable.transform.rotation = Quaternion.LookRotation(throwableTarget - cloneThrowable.transform.position);

    //    float elapse_time = 0;


    //    while (elapse_time < flightDuration)
    //    {
    //        if (cloneThrowable == null)
    //            yield break;


    //        cloneThrowable.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

    //        elapse_time += Time.deltaTime;

    //        yield return null;
    //    }
    //}
}






