using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectCollision : MonoBehaviour
{
    GameObject player;
    public bool isHit;
    public AudioClip m_BottleHit;
    public AudioClip m_BottleBreak;

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
            AudioSource.PlayClipAtPoint(m_BottleBreak, this.gameObject.transform.position);
            Destroy(this.gameObject);
        }

    }

    private void ThrowableHit()
    {

        if (player == null) return;

        PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();

        if (playerLogic == null) return;

        Debug.Log("Damage player");

        playerLogic.TakeDamage();

        AudioSource.PlayClipAtPoint(m_BottleBreak, this.gameObject.transform.position);

        Destroy(this.gameObject);
    }


}
