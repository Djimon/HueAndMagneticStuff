using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoundMap : MonoBehaviour {

    public float lineWidth = 0.1f;
    public float foreGroundThreshold = -10.0f;

    public GameObject switchIcon;
    public GameObject sourceIcon;

    private Switch[] switches,switches1, switches2, switches3, switches4;
	// Use this for initialization
	void Start ()
    {
        switches1 = FindObjectsOfType<Switch>();
        switches2 = FindObjectsOfType<ToggleSwitch>();
        switches3 = FindObjectsOfType<TimedSwitch>();
        switches4 = FindObjectsOfType<FlipFlop>();
        switches = switches1.Concat(switches2.Concat(switches3.Concat(switches4).ToArray()).ToArray()).ToArray();
        switches = switches.Distinct().ToArray();


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

            //TODO: Draw Icons for Switches and lights
            GameObject iconSwitch = Instantiate(switchIcon,switches[i].transform);
            iconSwitch.GetComponent<SpriteRenderer>().color = target.Color;
            iconSwitch.tag = "icon";
            iconSwitch.transform.position = new Vector3(pos1.x, pos1.y, foreGroundThreshold);
            iconSwitch.transform.position = new Vector3(iconSwitch.transform.position.x, iconSwitch.transform.position.y, foreGroundThreshold);
            iconSwitch.SetActive(false);
            GameObject iconSource = Instantiate(sourceIcon,switches[i].transform);
            iconSource.GetComponent<SpriteRenderer>().color = target.Color;
            iconSource.tag = "icon";
            iconSource.transform.position = new Vector3(pos2.x,pos2.y, foreGroundThreshold);
            iconSource.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
