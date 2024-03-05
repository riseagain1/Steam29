using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEventScript : MonoBehaviour
{
    public GameObject window;
    private HingeJoint windowHinge;
    public GameObject head;
    public GameObject light;

    private bool played;

    // Start is called before the first frame update
    void Start()
    {
        windowHinge = window.GetComponent<HingeJoint>();
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Door angle" + door.transform.rotation.eulerAngles.y);
        if(windowHinge.angle > 90 && !played){
            played = true;
            Debug.Log("window opened");
            head.SetActive(true);
            light.SetActive(false);
            StartCoroutine(DisableAfterDelay(1.1f));
        }
    }

    public void WindowGrabbed(){
        Debug.Log("window grabbed");
        
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
