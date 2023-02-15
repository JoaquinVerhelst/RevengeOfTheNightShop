using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayRandomSound : MonoBehaviour
{
    public AudioSource m_AudioSource;
    public AudioClip[] m_SoundClips;
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
        if (m_AudioSource != null && m_SoundClips.Length != 0)
        {
            int soundClipToUse = Random.Range(1, m_SoundClips.Length);
            m_AudioSource.clip = m_SoundClips[soundClipToUse];
            m_AudioSource.Play();
        }
    }
}
