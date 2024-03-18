using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialManager : MonoBehaviour
{
    public GameObject interactions;
    public List<Sprite> pages;
    private int pagesAdded = 0;

    public XRGrabInteractable reverser;
    public GameObject tutorialReverser;

    public XRGrabInteractable CylinderValve;
    public GameObject tutorialCylinderValve;

    public GameObject tutorialFireBox;

    public XRGrabInteractable brakeLever;
    public GameObject tutorialBrakeLever;

    public XRGrabInteractable throttle;
    public GameObject tutorialThrottle;

    // enable this step once
    public bool drained = false;
    
    void Start()
    {
        reverser.enabled = false;
        CylinderValve.enabled = false;
        brakeLever.enabled = false;
        throttle.enabled = false;


        // ENABLE REVERSER
        interactions.GetComponent<bookEventScript>().outlinePage = 0;
        interactions.GetComponent<bookEventScript>().addPage(pages[0]);
        
        pagesAdded++;
        reverser.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        // reverser step
        if (pagesAdded >= 1){
            if(interactions.GetComponent<TrainControl>().johnForward()){
                if(pagesAdded == 1){ // now cyl valve and coal
                    interactions.GetComponent<bookEventScript>().outlinePage = 1;
                    interactions.GetComponent<bookEventScript>().addPage(pages[1]);
                    
                    pagesAdded++;
                    CylinderValve.enabled = true;
                    interactions.GetComponent<FireBoxScript>().enableFirstCoal();
                }
                
                
                tutorialReverser.SetActive(false);
            } else {
                tutorialReverser.SetActive(true); // make sure player keeps it forward
            }
        }

        // cyl valve and coal
        if( pagesAdded >= 2){

            // cyl valve step
            if(interactions.GetComponent<TrainControl>().cylValveTurned()){
                drained = true;

            } else if (drained && !(interactions.GetComponent<TrainControl>().cylValveTurned())){ // need not turned
                tutorialCylinderValve.SetActive(false); // make sure player keeps cyl on
            } else {
                tutorialCylinderValve.SetActive(true);
            }
            
            // coal step
            if(interactions.GetComponent<FireBoxScript>().enoughHeat()){ // enough coal added
    
                tutorialFireBox.SetActive(false);
            } else {
                tutorialFireBox.SetActive(true);
            }

            // now brakes and throttle
            if(pagesAdded == 2 && !tutorialFireBox.activeSelf && !tutorialCylinderValve.activeSelf){
                interactions.GetComponent<bookEventScript>().outlinePage = 2;
                interactions.GetComponent<bookEventScript>().addPage(pages[2]);
                pagesAdded++;
                brakeLever.enabled = true;
                throttle.enabled = true;
            }

        }
        
        // brake and throttle

        if( pagesAdded >= 3){
            // brake step
            if(!interactions.GetComponent<TrainControl>().brakeApplied()){ // brake removed
    
                tutorialBrakeLever.SetActive(false);
            } else {
                tutorialBrakeLever.SetActive(true);
            }

            // throttle step
            if(interactions.GetComponent<TrainControl>().throttlePulled()){ // brake removed
    
                tutorialThrottle.SetActive(false);
            } else {
                tutorialThrottle.SetActive(true);
            }

            // check if all things in check
            if(!tutorialReverser.activeSelf 
            && !tutorialFireBox.activeSelf 
            && !tutorialCylinderValve.activeSelf
            && !tutorialBrakeLever.activeSelf
            && !tutorialThrottle.activeSelf){
                Debug.Log("TUTORIAL PASSED!");
                StartCoroutine(MyFunctionWithDelayHI(2f));
            }
        }
    }

    IEnumerator MyFunctionWithDelayHI(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Your code here
        Debug.Log("Function called after 2 seconds.");
        GameObject.Find("SceneChanger").GetComponent<SceneChanger>().startGame();
    }
}
