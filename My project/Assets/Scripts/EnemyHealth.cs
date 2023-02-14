using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int m_Health = 1;
    void Start()
    {

    }

    // Update is called once per frame
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
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerLogic>().TakeDamage();
        }
    }
}
