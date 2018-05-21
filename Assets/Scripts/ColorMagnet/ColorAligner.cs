using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorAligner : MonoBehaviour
{
    [System.Serializable]
    public class ColorVector2
    {
        public Color Color = Color.red;
        public Vector2 Vector = Vector2.up;
    }

    public List<ColorVector2> AlignVectors = new List<ColorVector2>() { new ColorVector2() };

    ColorSourceManager ColorSourceManager;

    [Range(0F, 1F)]
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
            Vector2 evaluatedVectorForCurrentColor = ColorSourceManager.EvaluateColorSourcesAt(transform.position, alignVector.Color);
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


            Vector3 alignmentDirection = (transform.TransformVector(alignmentVector.Vector)).normalized;
            Vector3 alignmentNormal = new Vector2(alignmentDirection.y, -alignmentDirection.x);

            Gizmos.DrawLine(transform.position, transform.position + alignmentDirection);
            Gizmos.DrawLine(transform.position + alignmentDirection, transform.position + 0.9F * alignmentDirection + 0.1F * alignmentNormal);
            Gizmos.DrawLine(transform.position + alignmentDirection, transform.position + 0.9F * alignmentDirection - 0.1F * alignmentNormal);
        }

        Gizmos.color = tmp;
    }
}
