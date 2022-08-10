using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class Generate : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator gen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (gen.Update)
            {
                gen.Generate();
            }
        }
        if (GUILayout.Button("Generate"))
        {
            gen.Generate();
        }
        if (GUILayout.Button("Delete"))
        {
            gen.Delet();
        }
    }
}
