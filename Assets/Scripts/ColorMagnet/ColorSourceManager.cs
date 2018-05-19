using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSourceManager : MonoBehaviour
{
    List<ColorSource> PrimaryColorSources = new List<ColorSource>();
    List<ColorSource> SecondaryColorSources = new List<ColorSource>();

    public void RegisterColorSource(ColorSource colorSource)
    {
        if(colorSource is SecondaryColorSource)
        {
            return;
        }
        
        foreach (ColorSource otherColorSource in PrimaryColorSources)
        {
            SecondaryColorSources.Add(SecondaryColorSource.LerpInstantiate(colorSource, otherColorSource, 0.5F, this.transform));
        }

        PrimaryColorSources.Add(colorSource);
    }

    public List<ColorSource> GetColorSources(Color color)
    {
        List<ColorSource> result = PrimaryColorSources.FindAll(c => c.Color == color);
        result.AddRange(SecondaryColorSources.FindAll(c => c.Color == color && (c as SecondaryColorSource).Parent1.isActiveAndEnabled && (c as SecondaryColorSource).Parent2.isActiveAndEnabled));

        return result;
    }
}
