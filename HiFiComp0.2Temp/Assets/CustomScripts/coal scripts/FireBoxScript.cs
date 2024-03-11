using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoxScript : MonoBehaviour
{
    // https://learn.unity.com/tutorial/introduction-to-object-pooling#
    public GameObject poolObj;
    public GameObject poolHolder;
    public List<GameObject> pool;
    private int poolSize;
    private int currentIndex;
    private Vector3 originalPosition;

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
            temp = Instantiate(poolObj, poolHolder.transform);
            temp.SetActive(false);
            pool.Add(temp);
        }

        originalPosition = poolObj.transform.position;

        
        // enable first one
        pool[0].SetActive(true);
        currentIndex = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void coalPickedUp(){
        pool[currentIndex].transform.position = originalPosition;
        pool[currentIndex].SetActive(true);
        currentIndex++;
        if(currentIndex >= poolSize){
            currentIndex = 0;
        }
    }
}
