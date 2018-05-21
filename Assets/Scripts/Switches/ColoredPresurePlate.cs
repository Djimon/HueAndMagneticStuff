using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredPresurePlate : MonoBehaviour {

    public Color Color = Color.red;

    List<ColorSource> ColorSources = new List<ColorSource>();

    bool isOn = false;

    SpriteRenderer SpriteRenderer;

    private void Start()
    {
        ColorSources = new List<ColorSource>(FindObjectsOfType<ColorSource>()).FindAll(cSource => cSource.Color == Color);

        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SpriteRenderer.color = Color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOn = !isOn;

        foreach (ColorSource colorSource in ColorSources)
        {
            colorSource.IsEmitting = isOn;
        }

    }
}
