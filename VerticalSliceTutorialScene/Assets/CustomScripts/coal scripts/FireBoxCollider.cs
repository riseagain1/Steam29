using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoxCollider : MonoBehaviour
{
    public GameObject interactions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a specific object/tag
        if(other.CompareTag("coal"))
        {
            Debug.Log("coal placed into firebox");
            interactions.GetComponent<FireBoxScript>().coalAddedToFire(other);
        }
    }
}
