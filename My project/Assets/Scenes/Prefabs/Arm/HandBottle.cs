using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.ParticleSystem;

public class HandBottle : MonoBehaviour
{
    public bool isHit;
    ParticleSystem particles;
    //ParticleSystem thisObectsParticles;
    //[SerializeField] GameObject thisObject;
    [SerializeField] List<GameObject> BottleObjects = new List<GameObject>();

    public GameObject currentBottle;
    int index;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }


    public void InstantiateBottle()
    {
        index = Random.Range(0, BottleObjects.Count);
        currentBottle = Instantiate(BottleObjects[index], this.transform);
    }

    public void GetDestroyed()
    {
        if (currentBottle != null)
        {
            //particles.Play();
            Destroy(currentBottle);
        }

    }

    //TODO rotate the transform -> instead of destroying this object -> destroy the child (bottleObject)

}
