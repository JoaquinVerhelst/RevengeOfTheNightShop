using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int m_Health = 1;


    void Update()
    {
        if (m_Health == 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void GetDamage()
    {
        --m_Health;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    PlayerLogic logic = other.gameObject.GetComponent<PlayerLogic>();
        //    logic.TakeDamage();
        //}
    }
}
