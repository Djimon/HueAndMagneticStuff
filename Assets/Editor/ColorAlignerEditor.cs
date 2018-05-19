using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorAligner))]
public class ColorAlignerEditor : Editor
{
    public void OnSceneGUI()
    {
        ColorAligner myTarget = (ColorAligner)target;

        foreach (ColorAligner.ColorVector2 cv in myTarget.AlignVectors)
        {
            cv.Vector = Handles.PositionHandle(myTarget.transform.position + (Vector3)cv.Vector.normalized, Quaternion.identity) - myTarget.transform.position;
        }
    }
}
