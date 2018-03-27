#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ResourceSpawner))]
public class ResourceSpawnerEditor : Editor {
    protected virtual void OnSceneGUI()
    {
        ResourceSpawner resTarget = (ResourceSpawner)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newSpawnLocation = Handles.PositionHandle(resTarget.transform.TransformPoint(resTarget.spawnPosition), Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(resTarget, "Change Spawn Position");
            resTarget.spawnPosition = resTarget.transform.InverseTransformPoint(newSpawnLocation);
        }
    }
}
#endif
