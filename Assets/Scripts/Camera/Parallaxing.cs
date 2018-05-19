using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform MainCam;
    public Transform[] backgrouds;
    private float[] parallaxScales;
    public float smoothing = 1f;


    private Vector3 prevCamPos;

    // Use this for initialization
    void Start()
    {
        prevCamPos = MainCam.position;

        parallaxScales = new float[backgrouds.Length];
        for (int i = 0; i < backgrouds.Length; i++)
        {
            parallaxScales[i] = backgrouds[i].position.z * -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrouds.Length; i++)
        {
            float parallax = (prevCamPos.x - MainCam.position.x) * parallaxScales[i];
            Vector3 targetPos = new Vector3(backgrouds[i].position.x + parallax, backgrouds[i].position.y, backgrouds[i].position.z);

            backgrouds[i].position = Vector3.Lerp(backgrouds[i].position, targetPos, smoothing * Time.deltaTime);
        }

        prevCamPos = MainCam.position;
    }
}
