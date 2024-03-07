using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnColliderScript : MonoBehaviour
{
    public GameObject rotateAnchor;
    public float turnDegrees;

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
        if(other.CompareTag("trainTurner"))
        {
            Debug.Log("Entered Trigger with train!");
            rotateAnchor.transform.rotation = Quaternion.Euler(0, turnDegrees, 0);
            
        }
    }
}
