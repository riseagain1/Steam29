using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerEventScript : MonoBehaviour
{
    public GameObject flicker;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

            flicker.SetActive(true);

    }
}
