using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(JSONexporter))]
public class JSONexporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        JSONexporter myScript = (JSONexporter)target;
        if (GUILayout.Button("Export JSON"))
        {
            myScript.ExportNodes();
        }
            }
}