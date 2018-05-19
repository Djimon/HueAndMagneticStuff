using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSource : MonoBehaviour
{
    public Vector2 Position { get { return transform.position; } protected set { transform.position = value; } }

    public float Intensity = 1F;

    public bool IsEmitting = false;

    public Color Color = Color.red;

    public float Radius = 10F;

    private void OnEnable()
    {
        FindObjectOfType<ColorSourceManager>().RegisterColorSource(this);
    }

    private void OnDrawGizmos()
    {
        Color tmp = Gizmos.color;
        Gizmos.color = Color;

        Gizmos.DrawIcon(Position, "Potato_" + Color.ToColorName(), true);
        
        Gizmos.color = tmp;
    }
}
