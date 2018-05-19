﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSource : MonoBehaviour
{
    public Vector2 Position { get { return transform.position; } protected set { transform.position = value; } }

    public float Intensity = 1F;

    public bool IsEmitting = false;

    public Color Color = Color.red;

    public float Radius = 10F;

    ColorSourceManager ColorSourceManager;

    protected void Update()
    {
        
    }

    private void OnEnable()
    {
        ColorSourceManager = FindObjectOfType<ColorSourceManager>();
        ColorSourceManager.RegisterColorSource(this);
    }

    private void OnDisable()
    {
        ColorSourceManager.UnregisterColorSource(this);
    }

    private void OnDrawGizmos()
    {
        Color tmp = Gizmos.color;
        Gizmos.color = Color;

        Gizmos.DrawIcon(Position, "Potato_" + Color.ToColorName(), true);
        
        Gizmos.color = tmp;
    }

    public void ToggleEmittor()
    {
        IsEmitting = !IsEmitting;
    }

    public float EvaluateIntensityAt(Vector2 position)
    {

        return IsEmitting ? Intensity * Mathf.Max(0F, (1F - Vector3.Distance(this.Position, position) / Radius)) : 0F;
    }
}
