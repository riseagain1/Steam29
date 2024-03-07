using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrainControl : MonoBehaviour
{
    public XRGrabInteractable lever; // Reference to the lever's XRGrabInteractable component
    public float maxSpeed = 10f; // Maximum speed of the train
    private float currentSpeed = 0f; // Current speed of the train
    private Vector3 initialLeverPosition; // Initial position of the lever

    void Start()
    {
        if (lever)
        {
            // Store the initial position of the lever
            initialLeverPosition = lever.transform.localPosition;
        }
    }

    void Update()
    {
        if (lever)
        {
            // Calculate the lever's displacement from its initial position
            float displacement = lever.transform.localPosition.z - initialLeverPosition.z;
            // Map the displacement to the train's speed
            currentSpeed = Mathf.Clamp(displacement * maxSpeed, 0, maxSpeed);
            
            // Move the train forward based on the current speed
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
    }
}
