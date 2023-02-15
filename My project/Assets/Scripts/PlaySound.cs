using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource m_AudioSource;
    public AudioClip m_SoundClips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.clip = m_SoundClips;
            m_AudioSource.Play();
        }
    }
}
