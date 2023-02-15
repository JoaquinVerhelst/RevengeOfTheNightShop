using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealthScript : MonoBehaviour
{

    public int health;



    public void TakeDamage(int damage)
    {

        health -= damage;

        if (health <= 0)
        {
            Debug.Log("U DEAD DEAD");
        }
    }




}
