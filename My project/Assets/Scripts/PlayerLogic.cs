using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerLogic : MonoBehaviour
{
    private int m_Health;

    private float m_MaxInvincibiltyTimeAfterAttack;
    private float m_InvincibiltyTime;
    private float m_MaxAttackCoolDownTime;
    private float m_AttackCoolDownTime;

    private float m_AttackTime;//To Replace With Animation

    private StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        m_Health = 3;
        m_MaxInvincibiltyTimeAfterAttack = 1.5f;
        m_MaxAttackCoolDownTime = 1.0f;
        m_AttackCoolDownTime = 0;
        m_InvincibiltyTime = 0;
        _input = GetComponent<StarterAssetsInputs>();
        m_AttackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.attack)
        {
            m_AttackTime = 0.5f;
            _input.attack = false;
        }

        if (m_AttackCoolDownTime > 0)
        {
            m_AttackCoolDownTime -= Time.deltaTime;
            if (m_AttackCoolDownTime < 0)
            {
                m_AttackCoolDownTime = 0;
            }
        }

        if (m_InvincibiltyTime > 0)
        {
            m_InvincibiltyTime -= Time.deltaTime;
            if (m_InvincibiltyTime < 0)
            {
                m_InvincibiltyTime = 0;
            }
        }

        //To Replace with Animation end
        if (m_AttackTime > 0)
        {
            m_AttackTime -= Time.deltaTime;
            if (m_AttackTime < 0)
            {
                m_AttackTime = 0;
            }
        }
    }

    public void TakeDamage()
    {
        if (m_InvincibiltyTime == 0)
        {
            Debug.Log("Damage");
            --m_Health;
            m_InvincibiltyTime = m_MaxInvincibiltyTimeAfterAttack;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && m_AttackCoolDownTime == 0 && m_AttackTime != 0)
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<EnemyHealth>().GetDamage();
            m_AttackCoolDownTime = m_MaxAttackCoolDownTime;
        }
    }
}
