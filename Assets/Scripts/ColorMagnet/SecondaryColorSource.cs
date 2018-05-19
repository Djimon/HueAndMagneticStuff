using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryColorSource : ColorSource
{
    public ColorSource Parent1;
    public ColorSource Parent2;

    public static SecondaryColorSource LerpInstantiate(ColorSource parent1, ColorSource parent2, float t, Transform parentTransform)
    {
        SecondaryColorSource lerpedColorSource = Instantiate(Resources.Load<SecondaryColorSource>("Secondary Color Source Template"), parentTransform);

        lerpedColorSource.Parent1 = parent1;
        lerpedColorSource.Parent2 = parent2;

        lerpedColorSource.Position = Vector3.Lerp(parent1.Position, parent2.Position, t);

        float intensity = 0F;
        intensity += parent1.Intensity * Mathf.Max(0F, (1F - Vector3.Distance(lerpedColorSource.Position, parent1.Position) / parent1.Radius));
        intensity += parent2.Intensity * Mathf.Max(0F, (1F - Vector3.Distance(lerpedColorSource.Position, parent2.Position) / parent2.Radius));
        lerpedColorSource.Intensity = intensity;

        lerpedColorSource.IsEmitting = true;

        lerpedColorSource.Color = parent1.Color + parent2.Color;
        lerpedColorSource.Color.a = Mathf.Lerp(parent1.Color.a, parent2.Color.a, t);

        lerpedColorSource.Radius = Mathf.Lerp(parent1.Radius, parent2.Radius, t);

        return lerpedColorSource;
    }
}
