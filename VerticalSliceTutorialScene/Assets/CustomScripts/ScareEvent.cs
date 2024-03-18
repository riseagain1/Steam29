using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareEvent : MonoBehaviour
{
    public GameObject flicker;
    public GameObject passenger;
    public Animator passAnim;
    private bool played;

    // Start is called before the first frame update
    void Start()
    {
        passAnim = passenger.GetComponent<Animator>();
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        private void OnTriggerEnter(Collider other)
    {
        
        if(!played){
            played = true;
            passAnim.Play("jump");
            flicker.GetComponent<FlickerGlitch>().timer = 100000;
            flicker.GetComponent<FlickerGlitch>().allGlitch();
            
            StartCoroutine(DisableAfterDelay(1f));
        }


    }

        IEnumerator DisableAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the object
        flicker.GetComponent<FlickerGlitch>().allFixed();
        flicker.SetActive(false);
    }
}
