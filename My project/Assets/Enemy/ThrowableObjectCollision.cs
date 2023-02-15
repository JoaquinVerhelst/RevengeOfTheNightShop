using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectCollision : MonoBehaviour
{
    GameObject player;
    public bool isHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            player = other.gameObject;
            ThrowableHit();

        }
        else if (other.CompareTag("World"))
        {
            Destroy(this.gameObject);
        }

    }

    private void ThrowableHit()
    {

        if (player == null) return;


        TestHealthScript testHealth = player.GetComponent<TestHealthScript>();
        if (testHealth == null) return;

        testHealth.TakeDamage(2);

        Destroy(this.gameObject);
    }


}
