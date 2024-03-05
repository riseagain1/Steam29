using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEventScript : MonoBehaviour
{
    public GameObject window;
    public GameObject head;
    public GameObject outhead;
    public GameObject light;

    private bool played;

    // Start is called before the first frame update
    void Start()
    {
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        // //Debug.Log("Door angle" + door.transform.rotation.eulerAngles.y);
        if(window.transform.position.z > 1.2 && !played){
            played = true;
            Debug.Log("window opened");
            outhead.SetActive(true);
            light.SetActive(false);
            StartCoroutine(DisableInitialFace(0.2f));
            StartCoroutine(DisableAfterDelay(1.1f));
        }
    }

    public void WindowGrabbed(){
        Debug.Log("window grabbed");
        
    }

    IEnumerator DisableInitialFace(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        head.SetActive(true);
        outhead.SetActive(false);
    }
    IEnumerator DisableAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the object
        head.SetActive(false);
        light.SetActive(true);
    }
}
