using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventScript : MonoBehaviour
{
    public GameObject door;
    public GameObject DirLight;
    public Material GoodSkyboxMaterial;
    public Material BadSkyboxMaterial;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Door angle" + door.transform.rotation.eulerAngles.y);
        if(door.transform.rotation.eulerAngles.y > 90){
            Debug.Log("Door opened");
            RenderSettings.skybox = BadSkyboxMaterial;
            DirLight.SetActive(false);
            //button.SetActive(true);

            StartCoroutine(MyFunctionWithDelay(2f));
            
        }
    }

    public void DoorGrabbed(){
        Debug.Log("Door grabbed");
        
    }

    IEnumerator MyFunctionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Your code here
        Debug.Log("Function called after 2 seconds.");
        GameObject.Find("SceneChanger").GetComponent<SceneChanger>().startTutorial();
    }
}
