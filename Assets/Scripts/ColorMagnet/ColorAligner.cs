using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
