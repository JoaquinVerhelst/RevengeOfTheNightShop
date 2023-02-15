using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectingBeer : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI collectText;
    public GameObject objectToDestroy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collectText.text = "Find the exit";
            Destroy(objectToDestroy);
            Destroy(this.gameObject);
        }
    }
}
