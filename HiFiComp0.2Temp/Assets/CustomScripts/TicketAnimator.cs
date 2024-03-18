using UnityEngine;
using System.Collections; 

public class TicketAnimator : MonoBehaviour
{
    private Animator animator;
    private bool isAnimationPlaying = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayTicketAnimation()
    {
        if (!isAnimationPlaying)
        {
            animator.Play("ticketout");
            isAnimationPlaying = true; // needed to switch this boolean to true, or else infinitely goes to start

     StartCoroutine(ResetAnimationFlagAfterDelay());
        }
    }

    private IEnumerator ResetAnimationFlagAfterDelay()
    {
        isAnimationPlaying = true;
        yield return new WaitForSeconds(5.0f); // Adjust this duration to match your animation's length
        isAnimationPlaying = false;
    }
}