using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundMap : MonoBehaviour {

    public float lineWidth = 0.1f;
    public float foreGroundThreshold = -5.0f;

    private Switch[] switches;
	// Use this for initialization
	void Start ()
    {
        switches = FindObjectsOfType<Switch>();

        for (int i = 0; i < switches.Length; i++)
        {
            //find target
            ColorSource target = switches[i].target;
            //generate crosspoint (x1,y2)
            Vector2 pos1 = switches[i].transform.position;
            Vector2 pos2 = target.transform.position;
            Vector2 intersection = new Vector2(pos1.x, pos2.y);
            Vector3[] Positions = new Vector3[] {new Vector3(pos1.x,pos1.y,foreGroundThreshold),
                                                 new Vector3(intersection.x,intersection.y,foreGroundThreshold),
                                                 new Vector3(pos2.x,pos2.y,foreGroundThreshold)};
            //draw lines (linerenderer)
            LineRenderer lineRenderAtSwitch = switches[i].gameObject.AddComponent<LineRenderer>() as LineRenderer;
            lineRenderAtSwitch.positionCount = 3;
            lineRenderAtSwitch.SetPositions(Positions);
            lineRenderAtSwitch.widthMultiplier = lineWidth;
            lineRenderAtSwitch.material = Resources.Load<Material>("Materials/Line");
            lineRenderAtSwitch.material.color = target.Color;
            lineRenderAtSwitch.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
