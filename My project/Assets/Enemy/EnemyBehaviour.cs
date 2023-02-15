using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform targetTransform;
    private GameObject cloneThrowable;


    [SerializeField] GameObject throwableObject;
    [SerializeField] Transform throwablePlacement;
    [SerializeField] private float timerToThrow = 3f;
    [SerializeField] private float minDistanceToThrow = 15f;
    [SerializeField] private float maxDistanceToThrow = 30f;

    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    LayerMask worldLayer;

    Vector3 throwableTarget;

    private float toThrowTimer;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        worldLayer = LayerMask.GetMask("World");
    }


    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }



    private void Update()
    {
        //MoveTowardsPlayer();

        toThrowTimer += Time.deltaTime;

        if (CheckToThrow() && toThrowTimer >= timerToThrow)
        {
            if (cloneThrowable == null)
            {
                SpawnAndHoldBottle();
                throwableTarget = targetTransform.position;
                StartCoroutine(SimulateProjectile());
                toThrowTimer = 0;
            }

        }


        //To Do -> finalizing -> throwableTarget should only be initialized once  (when enemy throws)
        //                    -> Check if there is no object between enemy and player (Physics raycast)

    }

    public void MoveTowardsPlayer()
    {
        if (targetTransform != null)
        {
            Vector3 target = targetTransform.transform.position;
            agent.SetDestination(target);
        }
    }



    private void SpawnAndHoldBottle()
    {
        cloneThrowable = Instantiate(throwableObject, throwablePlacement);
    }

    private bool CheckToThrow()
    {
        Vector3 directionToTarget = targetTransform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        float distance = directionToTarget.magnitude;

        if (Mathf.Abs(angle) < 60 && distance > minDistanceToThrow && distance < maxDistanceToThrow)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, directionToTarget, maxDistanceToThrow, worldLayer))
                return false;
            else
                return true;
        }
       
        return false;
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        //throwableObject.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

        if (cloneThrowable == null)
            yield break;



        // Calculate distance to target
        float target_Distance = Vector3.Distance(cloneThrowable.transform.position, throwableTarget);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        cloneThrowable.transform.rotation = Quaternion.LookRotation(throwableTarget - cloneThrowable.transform.position);

        float elapse_time = 0;


        while (elapse_time < flightDuration)
        {
            if (cloneThrowable == null)
                yield break;


            cloneThrowable.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }



    }



}






