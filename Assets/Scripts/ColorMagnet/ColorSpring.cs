using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorSpring : MonoBehaviour
{
    public float maxLength = 10F;

    [Range(0F, 1F)]
    public float stiffness = 0.5F;

    [Range(0F, 1F)]
    public float speed = 0.1F;

    public bool FreezeX = false;
    public bool FreezeY = false;

    public List<Color> Colors = new List<Color>() { Color.red };

    Vector2 previousTargetOffset = Vector2.zero;
    
    ColorSourceManager ColorSourceManager;

    private void Start()
    {
        ColorSourceManager = FindObjectOfType<ColorSourceManager>();
    }

    private void Update()
    {
        Vector2 targetOffset = Vector2.zero;

        float summedIntensityWeight = 0F;

        foreach (Color color in Colors)
        {
            foreach (ColorSource colorSource in ColorSourceManager.GetColorSources(color))
            {
                float evaluatedIntensity = colorSource.EvaluateIntensityAt(transform.position);

                if (summedIntensityWeight == 0F)
                {
                    targetOffset = colorSource.Position - (Vector2)transform.position;
                }
                else
                {
                    targetOffset = Vector2.Lerp(colorSource.Position - (Vector2)transform.position, targetOffset, evaluatedIntensity / summedIntensityWeight);
                }

                summedIntensityWeight += evaluatedIntensity;
            }
        }

        if(summedIntensityWeight == 0F)
        {
            targetOffset = Vector2.zero;
        }

        float distanceToTarget = targetOffset.magnitude;

        targetOffset *= summedIntensityWeight;
        
        targetOffset = Vector2.ClampMagnitude(targetOffset * maxLength * (1F - stiffness), distanceToTarget);

        targetOffset = Vector2.ClampMagnitude(targetOffset, maxLength);

        targetOffset = Vector2.Lerp(previousTargetOffset, targetOffset, speed);

        if (FreezeX)
            targetOffset.x = 0;
        if (FreezeY)
            targetOffset.y = 0;


        foreach (Transform childTransform in GetComponentInChildren<Transform>())
        {
            childTransform.localPosition += (Vector3)(targetOffset - previousTargetOffset);
        }

        previousTargetOffset = targetOffset;
    }


    private void OnDrawGizmos()
    {
        if (Colors.Count > 0)
        {
            Color tmp = Gizmos.color;
            Gizmos.color = Colors[0];

            Gizmos.DrawIcon(transform.position, "Spring/Spring_" + Colors[0].ToColorName(), true);

            if (Application.isPlaying)
            {
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)previousTargetOffset);

            }

            Gizmos.color = Color.white - (Color.black * 0.95F);

            Gizmos.DrawWireSphere(transform.position, maxLength);

            Gizmos.color = tmp;

        }
    }
}
