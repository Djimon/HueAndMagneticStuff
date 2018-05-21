using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryColorSource : ColorSource
{
    public ColorSource Parent1;
    public ColorSource Parent2;

    private new void Update()
    {
        base.Update();

        LerpOnParents(0.5F);

        transform.localScale = Vector3.one * 0.5F * Radius;
    }

    public void LerpOnParents(float t)
    {
        Position = Vector3.Lerp(Parent1.Position, Parent2.Position, t);
        
        Intensity = Parent1.EvaluateIntensityAt(Position) + Parent2.EvaluateIntensityAt(Position);

        IsEmitting = Parent1.IsEmitting && Parent2.IsEmitting;
        
        Color = new Color(Mathf.Min(Parent1.Color.r + Parent2.Color.r, 1F), Mathf.Min(Parent1.Color.g + Parent2.Color.g, 1F), Mathf.Min(Parent1.Color.b + Parent2.Color.b, 1F), Mathf.Lerp(Parent1.Color.a, Parent2.Color.a, t));

        Radius = Mathf.Lerp(Parent1.Radius, Parent2.Radius, t);
    }

    public static SecondaryColorSource LerpInstantiate(ColorSource parent1, ColorSource parent2, float t, Transform parentTransform)
    {
        SecondaryColorSource lerpedColorSource = Instantiate(Resources.Load<SecondaryColorSource>("Secondary Color Source Template"), parentTransform);

        lerpedColorSource.Parent1 = parent1;
        lerpedColorSource.Parent2 = parent2;

        lerpedColorSource.LerpOnParents(t);

        return lerpedColorSource;
    }
}
