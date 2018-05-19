using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorAligner : MonoBehaviour
{
    [System.Serializable]
    public class ColorVector2
    {
        public Color Color = Color.black;
        public Vector2 Vector;
    }

    public List<ColorVector2> AlignVectors = new List<ColorVector2>();

    ColorSourceManager ColorSourceManager;

    public float rotationSpeed = 0.01F;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        ColorSourceManager = FindObjectOfType<ColorSourceManager>();
        DrawSprite();
    }

    private void Update()
    {
        float targetRotationAngle = 0F;

        float summedIntensityWeight = 0F;

        foreach (ColorVector2 alignVector in AlignVectors)
        {
            List<ColorSource> colorSources = new List<ColorSource>(ColorSourceManager.GetColorSources(alignVector.Color));

            Vector2 evaluatedVectorForCurrentColor = EvaluateColorSourcesAt(transform.position, colorSources);
            float rotationAngleForCurrentColor = Vector2.SignedAngle(transform.TransformVector(alignVector.Vector), evaluatedVectorForCurrentColor);
            float intensityForCurrentColor = evaluatedVectorForCurrentColor.magnitude;

            if (intensityForCurrentColor > float.Epsilon)
            {
                summedIntensityWeight += intensityForCurrentColor;

                targetRotationAngle = Mathf.Lerp(targetRotationAngle, rotationAngleForCurrentColor, intensityForCurrentColor / summedIntensityWeight);
            }
        }

        transform.Rotate(Vector3.forward, targetRotationAngle * rotationSpeed);
        
    }

    private static Vector2 EvaluateColorSourcesAt(Vector2 position, List<ColorSource> colorSources)
    {
        Vector2 result = Vector2.zero;

        foreach(ColorSource colorSource in colorSources)
        {
            result += (colorSource.Position - position).normalized * colorSource.EvaluateIntensityAt(position);
        }

        return result;
    }

    void DrawSprite()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = Color.white;
        spriteRenderer.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        Color tmp = Gizmos.color;

        foreach(ColorVector2 alignmentVector in AlignVectors)
        {
            Gizmos.color = alignmentVector.Color;
            Gizmos.DrawLine(transform.position, transform.position + (transform.TransformVector(alignmentVector.Vector)).normalized);
        }

        Gizmos.color = tmp;


        
    }

    //// Editor script
    //private void OnSceneGUI()
    //{
    //    Debug.Log("raiunedtrainedtrunietrainet");
    //    UnityEditor.Handles.DoPositionHandle(this.transform.position, this.transform.rotation);
    //    UnityEditor.Handles.RadiusHandle(Quaternion.identity, transform.position, 1.0f, false);
    //    Debug.Log("nudtriane");
    //}

    public void OnSceneGUI()
    {
        
            if (Handles.Button(transform.position, Quaternion.identity, 0.25f, 0.25f, new Handles.CapFunction((i, v, q, f, e) => Debug.Log("unidrea") )))
            {
                transform.position = Handles.PositionHandle(transform.position, Quaternion.identity);
            
           }
        
    }
}
