using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrainControl : MonoBehaviour
{
    public GameObject environment;
    public GameObject rotateAnchor;

    public GameObject johnLever;
    public BoxCollider johnHandle;
    private HingeJoint johnLeverHinge;

    public GameObject throttleLever;
    private HingeJoint throttleLeverHinge;

    public GameObject brakeLever;
    private HingeJoint brakeLeverHinge;

    public GameObject cylCock;
    private HingeJoint cylCockHinge;

    public ParticleSystem steam;


    private float maxSpeed = 20f; // Maximum speed of the train
    private float currentSpeed = 0f; // Current speed of the train
    private float initialJohnLeverPosition; // Initial position of the john lever
    private float johnDisplacement;
    
    private float initialThrottleLeverPosition; // Initial position of the throttle lever
    private float throttleDisplacement;

    public float speed;

    void Start()
    {

        johnLeverHinge = johnLever.GetComponent<HingeJoint>();
        throttleLeverHinge = throttleLever.GetComponent<HingeJoint>();
        brakeLeverHinge = brakeLever.GetComponent<HingeJoint>();
        cylCockHinge = cylCock.GetComponent<HingeJoint>();

        if (johnLever)
        {

            // Store the initial position of the john lever
            initialJohnLeverPosition = johnLeverHinge.angle;
            johnDisplacement = johnLeverHinge.angle - initialJohnLeverPosition;
        }

        if (throttleLever){

            // store init pos of throttle
            initialThrottleLeverPosition = throttleLeverHinge.angle;
            throttleDisplacement = throttleLeverHinge.angle - initialThrottleLeverPosition;
        }


    }

    void Update()
    {
        
    }

    void FixedUpdate(){
        // Vector3 movement = rotateAnchor.transform.rotation * (new Vector3(0, 0, 5 * Time.deltaTime));
        // environment.transform.Translate((new Vector3(movement.x, movement.y, -movement.z)));

        // Calculate the lever's angle displacement from its initial position
        johnDisplacement = johnLeverHinge.angle - initialJohnLeverPosition;
        throttleDisplacement = throttleLeverHinge.angle - initialThrottleLeverPosition;
       

        if(GetComponent<FireBoxScript>().enoughHeat()) { // enough heat to produce steam
            
            if(cylValveTurned()){ // cock is open
                steam.Play();
                currentSpeed -= 10 * Time.deltaTime; // release steam and water
            }
         
            // apply throttle
            currentSpeed += (throttleDisplacement / 2.0f) * Time.deltaTime;

        
        }

        if(cylValveTurned()){ // cock is open
            // drain water from engine
        }
        
        // less steam when brakes applied + slowdown
        if (brakeApplied()){
            currentSpeed -= 5 * Time.deltaTime; 
        }

        // always slowing down due to rolling friction
        currentSpeed -= 3 * Time.deltaTime;
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); // clamp values
        Debug.Log("current speed:" + currentSpeed);

        if(currentSpeed > 0.5f){ // can't move johnson bar while moving
            johnHandle.enabled = false; // disable collider
        } else {
            johnHandle.enabled = true; // enable
        }

        // Move the train based on current speed and reverser position
        if(johnForward()){ // go forward
            Vector3 movement = rotateAnchor.transform.rotation * (new Vector3(0, 0, currentSpeed * Time.deltaTime));
            environment.transform.Translate((new Vector3(movement.x, movement.y, -movement.z)));
        }

        if(johnDisplacement < -4.0f){ // go in reverse
            Vector3 movement = rotateAnchor.transform.rotation * (new Vector3(0, 0, -currentSpeed * Time.deltaTime));
            environment.transform.Translate((new Vector3(movement.x, movement.y, -movement.z)));
        }
        

        //Debug.Log("brake angle:" + brakeLeverHinge.angle);
        //Debug.Log("cyl cock angle:" + cylCockHinge.angle);
        //Debug.Log("johnDisplacement: " + johnDisplacement);
        //Debug.Log("throttleDisplacement: " + throttleDisplacement);

    }


    public bool johnForward(){
        if(johnDisplacement > 4.0f){
            return true;
        }
        return false;
    }

    public bool cylValveTurned(){
        if( cylCockHinge.angle > 110){
            return true;
        }
        return false;
    }

    public bool brakeApplied(){
        if(brakeLeverHinge.angle < 50){
            return true;
        }
        return false;
    }

    public bool throttlePulled(){
        if(throttleDisplacement > 2.0f){
            return true;
        }
        return false;
    }
}
