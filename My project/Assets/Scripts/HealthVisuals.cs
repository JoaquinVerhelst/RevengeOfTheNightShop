using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] m_HealthImages;
    public Sprite m_HealthLostImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeImage(int currentHealth)
    {
        m_HealthImages[currentHealth].sprite = m_HealthLostImage;
    }
}
