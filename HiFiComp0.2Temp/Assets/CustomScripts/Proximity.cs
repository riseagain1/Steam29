using UnityEngine;

public class Proximity : MonoBehaviour
{
    public TicketAnimator ticketAnimator; 

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has entered the trigger zone.");
        ticketAnimator.PlayTicketAnimation();
    }
}
