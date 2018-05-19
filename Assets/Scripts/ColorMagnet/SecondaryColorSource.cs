﻿using System.Collections;
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
    }

    public void LerpOnParents(float t)
    {
        Position = Vector3.Lerp(Parent1.Position, Parent2.Position, t);

        float intensity = Intensity = Parent1.EvaluateIntensityAt(Position) + Parent2.EvaluateIntensityAt(Position);

        IsEmitting = true;

        Color = Parent1.Color + Parent2.Color;
        Color.a = Mathf.Lerp(Parent1.Color.a, Parent2.Color.a, t);

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
