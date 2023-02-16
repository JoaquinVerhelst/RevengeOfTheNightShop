using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICutscene : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject m_GameUI;
    public GameObject m_Textbox;
    public GameObject m_Count;
    public GameState m_State;

    private float m_TimerTextBox;
    private float m_TimeBetweenCount;
    private float m_MaxTimeBetweenCount;
    private int m_Num = 5;
    void Start()
    {
        m_GameUI.SetActive(false);
        m_Textbox.SetActive(true);
        m_Count.SetActive(false);

        m_Count.GetComponent<TextMeshProUGUI>().text = m_Num.ToString();

        m_TimerTextBox = 3;
        m_MaxTimeBetweenCount = 1; 
        m_TimeBetweenCount = m_MaxTimeBetweenCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Textbox.activeSelf == true)
        {
            m_TimerTextBox -= Time.deltaTime;
            if (m_TimerTextBox <= 0)
            {
                m_Textbox.SetActive(false);
                m_Count.SetActive(true);
            }
        }
        if (m_Count.activeSelf == true)
        {
            m_TimeBetweenCount -= Time.deltaTime;
            if (m_TimeBetweenCount <= 0)
            {
                --m_Num;
                if (m_Num == 0)
                {
                    m_Count.SetActive(false);
                    m_GameUI.SetActive(true);
                    //m_State.StartGame();
                }
                else
                {
                    m_Count.GetComponent<TextMeshProUGUI>().text = m_Num.ToString();
                    m_TimeBetweenCount = m_MaxTimeBetweenCount;
                }
            }
        }
    }
}
