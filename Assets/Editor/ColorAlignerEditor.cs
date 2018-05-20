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
            Handles.color = cv.Color;

            var handlePos = myTarget.transform.position + myTarget.transform.TransformVector(cv.Vector);


            // Buttons Color Change

            float buttonSize = HandleUtility.GetHandleSize(myTarget.transform.position) * 0.06f;
            float pickSize = buttonSize;

            // next Color
            Handles.color = cv.Color.NextColor();

            if (Handles.Button(handlePos + Vector3.right * 0.1F, Quaternion.identity, buttonSize, buttonSize, Handles.CubeHandleCap))
            {
                cv.Color = cv.Color.NextColor();
            }

            // previous Color
            Handles.color = cv.Color.PreviousColor();

            if (Handles.Button(handlePos + Vector3.left * 0.1F, Quaternion.identity, buttonSize, buttonSize, Handles.CubeHandleCap))
            {
                cv.Color = cv.Color.PreviousColor();
            }



            // Position Handle
            Handles.color = cv.Color;

            float handleSize = HandleUtility.GetHandleSize(myTarget.transform.position) * 0.2f;
            Vector3 snap = Vector3.one * 0.5f;

            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize, snap, Handles.SphereHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                cv.Vector = myTarget.transform.InverseTransformVector(newPos - myTarget.transform.position).normalized;
            }
        }
    }
}
