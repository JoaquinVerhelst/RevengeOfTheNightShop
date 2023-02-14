using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    private int m_Health;

    private float m_MaxInvincibiltyTimeAfterAttack;
    private float m_InvincibiltyTime;
    private float m_MaxAttackCoolDownTime;
    private float m_AttackCoolDownTime;

    //sprint
    private float m_MaxSprintTime;
    private float m_CurrentSprintTime;
    private float m_MaxSprintCoolDown;
    private float m_sprintCoolDown;

    private float m_AttackTime;//To Replace With Animation

    private StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        m_Health = 3;
        m_MaxInvincibiltyTimeAfterAttack = 1.5f;
        m_MaxAttackCoolDownTime = 0.6f;
        m_AttackCoolDownTime = 0;
        m_InvincibiltyTime = 0;
        _input = GetComponent<StarterAssetsInputs>();
        m_AttackTime = 0;

        m_MaxSprintTime = 0.3f;
        m_CurrentSprintTime = m_MaxSprintTime;
        m_MaxSprintCoolDown = 0.7f;
        m_sprintCoolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }

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

        SprintTimers();

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

    public float GetSprintTime()
    {
        return m_CurrentSprintTime;
    }

    private void SprintTimers()
    {
        if (_input.sprint)
        {
            m_CurrentSprintTime -= Time.deltaTime;
            if (m_CurrentSprintTime < 0)
            {
                m_CurrentSprintTime = 0;
                m_sprintCoolDown = m_MaxSprintCoolDown;
            }
        }

        if (m_sprintCoolDown > 0)
        {
            m_sprintCoolDown -= Time.deltaTime;
            if (m_sprintCoolDown < 0)
            {
                m_sprintCoolDown = 0;
                m_CurrentSprintTime = m_MaxSprintTime;
            }
        }

        if (!_input.sprint && m_CurrentSprintTime > 0)
        {
            m_CurrentSprintTime = m_MaxSprintTime;
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
