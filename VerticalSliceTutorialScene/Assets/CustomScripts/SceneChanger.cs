using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject KeepObj;

    // prevent dupe of singleton
    private static SceneChanger _instance;

    public static SceneChanger Instance { get { return _instance; }}

    // only one SceneChanger per scene
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
        
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene is '" + scene.name + "'.");

        // https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
        DontDestroyOnLoad(KeepObj);
        DontDestroyOnLoad(this.gameObject);

        //SceneManager.UnloadSceneAsync("GameplayScene");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(){
        Debug.Log("Starting game");
        SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("MenuScene");
    }

    public void openShop(){
        Debug.Log("Starting shop");
        SceneManager.LoadScene("ShopScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("GameplayScene");
    }

    public void closeShop(){
        Debug.Log("Starting game");
        SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("ShopScene");
    }

    public void openResults(){
        Debug.Log("Starting results");
        SceneManager.LoadScene("ResultsScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("GameplayScene");
    }

    public void closeResults(){
        Debug.Log("Starting game");
        SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("ResultsScene");
    }

    public void resultsToMenu(){
        Debug.Log("Starting Menu");
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("ResultsScene");
    }
}
