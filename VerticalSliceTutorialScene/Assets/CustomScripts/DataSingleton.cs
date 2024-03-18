using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSingleton : MonoBehaviour
{
    public int data;

    // prevent dupe of singleton
    private static DataSingleton _instance;

    public static DataSingleton Instance { get { return _instance; }}

    // only one DataSingleton per scene
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
