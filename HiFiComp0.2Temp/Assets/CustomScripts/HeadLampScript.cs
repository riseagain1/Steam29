using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLampScript : MonoBehaviour
{
    public GameObject lever;
    private HingeJoint leverHinge;
    
    public GameObject lampLeft;
    public GameObject lampRight;
    // Start is called before the first frame update
    void Start()
    {
        leverHinge = lever.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leverHinge.angle < 10){
            // headlamp off
            lampLeft.SetActive(false);
            lampRight.SetActive(false);
        }

        if(leverHinge.angle > 170){
            // headlamp on
            lampLeft.SetActive(true);
            lampRight.SetActive(true);
        }
    }
}
