using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessDDOL : MonoBehaviour
{

    public GameObject KeepHold;
    public GameObject ScenesHold;

    // Start is called before the first frame update
    void Start()
    {
        KeepHold = GameObject.Find("KeepObj");
        ScenesHold = GameObject.Find("SceneChanger");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        // ACCESS LLOD methods

    // SceneChanger
    public void GoToShop(){
        ScenesHold.GetComponent<SceneChanger>().openShop();
    }

    public void leaveShop(){
        ScenesHold.GetComponent<SceneChanger>().closeShop();
    }
    public void startGame(){
        ScenesHold.GetComponent<SceneChanger>().startGame();
    }

    public void GoToResults(){
         ScenesHold.GetComponent<SceneChanger>().openResults();
    }

    public void BacktoGameFromResults(){
        ScenesHold.GetComponent<SceneChanger>().closeResults();
    }

    public void backToMainFromResults(){
        ScenesHold.GetComponent<SceneChanger>().resultsToMenu();
    }

}
