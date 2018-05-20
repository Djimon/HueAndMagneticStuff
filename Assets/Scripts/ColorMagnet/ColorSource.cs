using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorSource : MonoBehaviour
{
    public Vector2 Position { get { return transform.position; } protected set { transform.position = value; } }

    [Range(0F, 1F)]
    public float Intensity = 1F;

    public bool IsEmitting = true;

    public Color Color = Color.red;

    public float Radius = 10F;

    ColorSourceManager ColorSourceManager;

    SpriteRenderer SpriteRendererReference;

    private void Start()
    {
        SpriteRendererReference = GetComponent<SpriteRenderer>();
        SpriteRendererReference.color = Color;
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

    protected void Update()
    {
        if (gameObject.GetComponent<SecondaryColorSource>() == null)
            Intensity = IsEmitting ? 1f : 0f;

        transform.localScale = Vector3.one * Radius;
        SpriteRendererReference.color = new Color(Color.r, Color.g, Color.b, Intensity);
    }

    private void OnDrawGizmos()
    {
        Color tmp = Gizmos.color;
        Gizmos.color = Color;

        Gizmos.DrawIcon(Position, "Potato_" + Color.ToColorName(), true);

        Gizmos.color = new Color(Color.r, Color.g, Color.b, Intensity * 0.25F);

        Gizmos.DrawWireSphere(Position, Radius);

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
