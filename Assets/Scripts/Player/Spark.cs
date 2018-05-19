using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {

    public Gradient colorGradient;
    public int changeSpeed = 7;

    [HideInInspector]
    public bool breakAnim = false;

    SpriteRenderer[] sparks;

    float time = 0;
    float duration = 1f;
    private Color baseColor;
    

    // Use this for initialization
    void Start ()
    {
        sparks = gameObject.GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        AnimateSpark();
    }

    private void AnimateSpark()
    {
        time += Time.deltaTime;
        float temp = (time % changeSpeed) / changeSpeed;
        //Debug.Log(temp);
        baseColor = colorGradient.Evaluate(temp);
        baseColor.a = 0.8f;

        for (int i = 0; i < sparks.Length; i++)
        {
            sparks[i].material.color = baseColor;
        }
    }
}
