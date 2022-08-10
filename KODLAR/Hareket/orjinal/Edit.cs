using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generator))]
public class Edit : Editor
{
    public override void OnInspectorGUI()
    {
        Generator mapGen = (Generator)target;

        if (DrawDefaultInspector())
        {
           // if (mapGen.autoUpdate)
            {
              //  mapGen.DrawMapInEditor();
            }
        }

        if (GUILayout.Button("Generate"))
        {
          //  mapGen.DrawMapInEditor();
        }
    }
}
