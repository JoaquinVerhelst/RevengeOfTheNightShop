using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string m_SceneName;
    public AudioSource m_AudioSource;
    public AudioClip m_SoundClips;
    private bool m_GoToNextScene = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AudioSource.isPlaying == false && m_GoToNextScene)
        {
            SceneManager.LoadScene(m_SceneName);
        }
    }

    public void GoToNextScene()
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.clip = m_SoundClips;
            m_AudioSource.Play();
        }
        m_GoToNextScene = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(m_SceneName);
    }
}
