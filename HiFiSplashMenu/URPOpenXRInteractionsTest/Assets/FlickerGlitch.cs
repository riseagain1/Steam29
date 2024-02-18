using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// borrowed from my CMPM125 script

public class FlickerGlitch : MonoBehaviour
{
    public bool startFlicker;
    public Material lightColor;
    private Color lightEmission;
    public GameObject Glitch;
    public GameObject Good;
    public GameObject lights;
    public GameObject lightsGlitch;

    public GameObject goodSprites;
   
    private int timer;

    // prevent dupe of singleton
    private static FlickerGlitch _instance;

    public static FlickerGlitch Instance { get { return _instance; }}

    // only one FlickerGlitch per scene
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
        startFlicker = true;
        timer = 100;

        lightEmission = new Color(212/255.0f, 174/255.0f, 111/255.0f);
        lightColor.SetColor("_EmissionColor", lightEmission);
    }

    // Update is called once per frame
    void Update()
    {
        if(startFlicker){
            if(timer <= 0){
                timer = Random.Range(10,200);
                float glitchChance = Random.Range(0,100);
                float tempIntens;

                if(glitchChance > 100){
                    tempIntens = Random.Range(1.0f,2.5f);
                } else {
                    tempIntens = Random.Range(0.0f,1.0f);
                }
                
                
                lightColor.SetColor("_EmissionColor", lightEmission * (tempIntens * 0.5f));

            foreach(Transform child in lights.transform){

                foreach(Transform child2 in child.transform){
                    child2.transform.GetComponent<Light>().intensity = tempIntens;
                }
                
            }

            foreach(Transform child in lightsGlitch.transform){

                foreach(Transform child2 in child.transform){
                    child2.transform.GetComponent<Light>().intensity = tempIntens;
                }
                
            }

            if(tempIntens > 0.8f){
                // fixed
                allFixed();
                
            } else {
                // glitch, broken
                Good.SetActive(false);
                Glitch.SetActive(true);

                foreach (Transform child in goodSprites.transform){
                    child.GetComponent<SpriteRenderer>().enabled = false;
                }

            }

            } else {
                timer--;
            }
        }
        
    }

    public void allFixed(){
        // fixed
        Good.SetActive(true);
        Glitch.SetActive(false);

        foreach (Transform child in goodSprites.transform){
                child.GetComponent<SpriteRenderer>().enabled = true;
        }

    }
}
