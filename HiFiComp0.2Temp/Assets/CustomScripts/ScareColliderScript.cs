using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareColliderScript : MonoBehaviour
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
    if(other.CompareTag("doorHandle"))
    {
        Debug.Log("Entered Trigger with handle");
        interactions.GetComponent<WindowEventScript>().WindowOpened();
    }
}
}
