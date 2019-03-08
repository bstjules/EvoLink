using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(JSONimporter))]
public class JSONimporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        JSONimporter myScript = (JSONimporter)target;

        if (GUILayout.Button("Import JSON"))
        {
            myScript.ImportNodes();
        }
    }
}