using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoxScript : MonoBehaviour
{
    // https://learn.unity.com/tutorial/introduction-to-object-pooling#
    public GameObject poolObj;
    //public GameObject poolHolder;
    public List<GameObject> pool;

    public GameObject dialHand;
    private int poolSize;
    private int currentIndex;
    private Vector3 originalPosition;

    private float dialAngle;
    public float GOAL_dialAngle;

    // prevent dupe of singleton
    private static FireBoxScript _instance;

    public static FireBoxScript Instance { get { return _instance; }}

    // only one FireBoxScript per scene
    private void Awake(){
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Awake();

         pool = new List<GameObject>();
         poolSize = 5;

        for(int i = 0; i < poolSize; i++)
        {
            GameObject temp;
            //https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
            temp = Instantiate(poolObj);
            temp.SetActive(false);
            pool.Add(temp);
        }

        originalPosition = poolObj.transform.localPosition;
        Debug.Log(poolObj.transform.localPosition);
        
        // // enable first one
        // pool[0].SetActive(true);
        // currentIndex = 1;

        dialAngle = 135; // empty
        GOAL_dialAngle = 120; // set to almost empty
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dialAngle > GOAL_dialAngle){ // go clockwise
            dialAngle -= 20 * Time.deltaTime;
            dialHand.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, dialAngle);
        }

        if(dialAngle < GOAL_dialAngle){ // go counter-clockwise
            dialAngle += 20 * Time.deltaTime;
            dialHand.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, dialAngle);
        }

        // always burning
        if(GOAL_dialAngle < 135){
            GOAL_dialAngle += 3 * Time.deltaTime;
        }
            

    }

    public void enableFirstCoal(){
        // enable first one
        pool[0].SetActive(true);
        currentIndex = 1;
    }

    public void coalPickedUp(){
        pool[currentIndex].transform.localPosition = originalPosition;
        pool[currentIndex].SetActive(true);
        currentIndex++;
        if(currentIndex >= poolSize){
            currentIndex = 0;
        }
    }

    public void coalAddedToFire(Collider obj){
        obj.gameObject.transform.localPosition = originalPosition;
        obj.gameObject.SetActive(false);
        

        GOAL_dialAngle -= 40; // move goal clockwise
        if(GOAL_dialAngle < -135){ // over max
            GOAL_dialAngle = -135;
        }
    }

    public float getDial(){
        return dialAngle;
    }

    public bool enoughHeat(){
        if(dialAngle < 80){
            return true;
        }
        return false;
    }
}
