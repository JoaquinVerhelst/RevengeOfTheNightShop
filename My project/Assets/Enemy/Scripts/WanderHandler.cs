using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class WanderHandler : MonoBehaviour
{
    [SerializeField] float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;


    Vector3 pos;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    public void UpdateWander()
    {
        timer += Time.deltaTime;
        agent.isStopped = false;

        if (timer >= wanderTimer)
        {
            //Vector3 newPos = RandomPosition(transform.position, wanderRadius, -1);
            SetUpNewPosition();
        }
        if (Vector3.Distance(pos, transform.position) <= 1)
        {
            SetUpNewPosition();
        }
    }

    private void SetUpNewPosition()
    {
        pos = RandomPosition(wanderRadius);
        agent.SetDestination(pos);
        timer = 0;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(pos, 0.3f);
    }



    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public Vector3 RandomPosition( float radius)
    {
        var randDirection = Random.insideUnitSphere * radius;
        randDirection += agent.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, radius, -1);
        return navHit.position;
    }
}
