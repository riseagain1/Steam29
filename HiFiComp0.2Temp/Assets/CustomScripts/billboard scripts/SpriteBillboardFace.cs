using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboardFace : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);

        //transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - 180, 0.0f);
    }
}
