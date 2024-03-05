using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordEventScript : MonoBehaviour
{
    public GameObject cord;

    public GameObject options;

    private Animator animator;
    
    public float NTime;

    public enum MyAnimationState{
        Idle,
        OptionsDown,
        OptionsUp,
    }

    public MyAnimationState clip;

    // Start is called before the first frame update
    void Start()
    {
        animator = options.GetComponent<Animator>();
        clip = MyAnimationState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(cord.transform.position.y < 1.6){ // cord pulled
            NTime = animator.GetCurrentAnimatorStateInfo (0).normalizedTime; // check if animation playing
                if(NTime > 1.0){ // clip done
                    cordPulled();
                }
        }
        
    }

    public void cordPulled(){
        Debug.Log("Cord pulled");

        switch (clip){
            case MyAnimationState.Idle:
                animator.Play("OptionsDown");
                clip = MyAnimationState.OptionsDown;
                break;

            case MyAnimationState.OptionsDown:
                animator.Play("OptionsUp");
                clip = MyAnimationState.OptionsUp;
                break;
            
            case MyAnimationState.OptionsUp:
                animator.Play("OptionsDown");
                clip = MyAnimationState.OptionsDown;
                break;
        }
    
    }

}
