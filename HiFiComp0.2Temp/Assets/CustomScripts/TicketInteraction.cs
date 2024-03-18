using UnityEngine;

public class TicketInteraction : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("VR Controller touched the ticket."); 
        animator.Play("ticketback");
    }
}
