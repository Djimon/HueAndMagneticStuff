using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorAligner))]
public class ColorAlignerEditor : Editor
{

    Vector3 pos = Vector3.zero;
    Quaternion rot = Quaternion.identity;

    public void OnSceneGUI()
    {
        ColorAligner myTarget = (ColorAligner)target;

        foreach (ColorAligner.ColorVector2 cv in myTarget.AlignVectors)
        {
            //cv.Vector = Handles.PositionHandle(myTarget.transform.position + (Vector3)cv.Vector.normalized, Quaternion.identity) - myTarget.transform.position;

            //Vector3 pos = myTarget.transform.position + (Vector3)cv.Vector.normalized;
          //Quaternion rot = Quaternion.identity;




            Handles.color = cv.Color;

            var handlePos = myTarget.transform.position + (Vector3)cv.Vector;

            float size = HandleUtility.GetHandleSize(myTarget.transform.position) * 0.06f;
            Vector3 snap = Vector3.one * 0.5f;

            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, size, snap, Handles.CircleHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                cv.Vector = (newPos - myTarget.transform.position).normalized;
            }
        }
    }
}
