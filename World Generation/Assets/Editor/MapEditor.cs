using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GenerateMap))]
public class MapEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GenerateMap mapGen = (GenerateMap)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.MapGeneration();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGen.MapGeneration();
        }
    }
}
