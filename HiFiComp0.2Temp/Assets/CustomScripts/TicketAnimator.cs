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
        }
    }
}
