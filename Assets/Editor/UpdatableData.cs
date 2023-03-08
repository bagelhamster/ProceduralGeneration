using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Updatable), true)]
public class UpdatableData : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Updatable data = (Updatable)target;

        if (GUILayout.Button("Update"))
        {
            data.NotifyOfUpdatedValues();
            EditorUtility.SetDirty(target);
        }
    }

}