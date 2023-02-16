using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class PlayerLogic : MonoBehaviour
{
    private int m_Health;

    //Invincibilty Frames
    private float m_MaxInvincibiltyTimeAfterAttack;
    private float m_InvincibiltyTime;

    //Attack
    private float m_MaxAttackCoolDownTime;
    private float m_AttackCoolDownTime;

    private float m_AttackTime;//To Replace With Animation

    //Sound
    public PlayRandomSound m_randomGlassSound;
    public PlayRandomSound m_randomHurtSound;

    //sprint
    private float m_MaxSprintTime;
    private float m_CurrentSprintTime;
    private float m_MaxSprintCoolDown;
    private float m_sprintCoolDown;


    private StarterAssetsInputs _input;

    public HealthVisuals m_Visuals;

    ArmHandler armHandler;
    HandBottle handBottle;

    [SerializeField] public int currentAmountOfBottles = 5;


    // Start is called before the first frame update
    void Start()
    {
        m_Health = 5;
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



        armHandler = GetComponentInChildren<ArmHandler>();
        handBottle = GetComponentInChildren<HandBottle>();


    }

    // Update is called once per frame
    void Update()
    {
        if (m_Health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }



        if (_input.attack && handBottle.currentBottle != null)
        {
            m_AttackTime = 0.6f;
            _input.attack = false;
            armHandler.Reinitialize();
        }
        else
            _input.attack = false;
        

        if (_input.reload && handBottle.currentBottle == null && currentAmountOfBottles > 0) 
        {
            Debug.Log("Reload");

            handBottle.InstantiateBottle();
            currentAmountOfBottles--;
            _input.reload = false;
        }


        armHandler.HandleSwingMovement();
        




        LowerTimer(ref m_AttackCoolDownTime);

        LowerTimer(ref m_InvincibiltyTime);

        SprintTimers();

        //To Replace with Animation end
        LowerTimer(ref m_AttackTime);
    }

    public void TakeDamage()
    {
        //doesn't take damage during invincibilty frames and during dodge
        if (m_InvincibiltyTime == 0 && !(!_input.sprint && m_CurrentSprintTime != m_MaxSprintTime))
        {
            Debug.Log("Damage");
            --m_Health;
            m_Visuals.ChangeImage(m_Health);
            m_randomHurtSound.playSound();
            m_InvincibiltyTime = m_MaxInvincibiltyTimeAfterAttack;
        }
    }

    public void OneHitKill()
    {
        SceneManager.LoadScene("GameOver");
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

    private void LowerTimer(ref float timer)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
            }
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && m_AttackCoolDownTime == 0 && m_AttackTime != 0 && handBottle.currentBottle != null)
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<EnemyHealth>().GetDamage();

            m_randomGlassSound.playSound();

            handBottle.GetComponent<HandBottle>().GetDestroyed();


            m_AttackCoolDownTime = m_MaxAttackCoolDownTime;
        }
    }

}



