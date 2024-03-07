using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrainControl : MonoBehaviour
{
    public GameObject environment;
    public GameObject rotateAnchor;

    public GameObject lever;
    private HingeJoint leverHinge;
    public float maxSpeed = 10f; // Maximum speed of the train
    private float currentSpeed = 0f; // Current speed of the train
    private float initialLeverPosition; // Initial position of the lever

    public float speed;

    void Start()
    {

        leverHinge = lever.GetComponent<HingeJoint>();
        if (lever)
        {

            // Store the initial position of the lever
            initialLeverPosition = leverHinge.angle;
        }
    }

    void Update()
    {
        
    }

    void FixedUpdate(){
        Vector3 movement = rotateAnchor.transform.rotation * (new Vector3(0, 0, 5 * Time.deltaTime));
        environment.transform.Translate((new Vector3(movement.x, movement.y, -movement.z)));


        // if (lever)
        // {
        //     // Calculate the lever's angle displacement from its initial position
        //     float displacement = leverHinge.angle - initialLeverPosition;
        //     Debug.Log("displacement: " + displacement);
        //     // Map the displacement to the train's speed
        //     currentSpeed = Mathf.Clamp(displacement, -maxSpeed, maxSpeed);
            
        //     // Move the train forward based on the current speed
        //     if(displacement > 1.0f || displacement < -1.0f){
        //         Vector3 movement = rotateAnchor.transform.rotation * (new Vector3(0, 0, currentSpeed * Time.deltaTime));
        //         environment.transform.Translate((new Vector3(movement.x, movement.y, -movement.z)));
        //     }
            
        // }
    }
}
