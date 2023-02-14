using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform targetTransform;
    SphereCollider attackSphere;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        attackSphere = GetComponent<SphereCollider>();
    }


    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }



    private void Update()
    {
        MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        if (targetTransform != null)
        {
            Vector3 target = new Vector3(targetTransform.position.x, 1, targetTransform.position.z);
            agent.SetDestination(target);
        }

    }





}
