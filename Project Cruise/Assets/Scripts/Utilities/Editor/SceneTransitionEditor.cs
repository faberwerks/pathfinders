using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneTransition))]
[CanEditMultipleObjects]
public class SceneTransitionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SceneTransition sceneTransition = target as SceneTransition;

        sceneTransition.transitionType = (SceneTransition.TransitionType)EditorGUILayout.EnumPopup("Transition Type", sceneTransition.transitionType);

        if (sceneTransition.transitionType == SceneTransition.TransitionType.KeyDown)
        {
            sceneTransition.keyToPress = (KeyCode) EditorGUILayout.EnumPopup("Key To Press", sceneTransition.keyToPress);
            sceneTransition.targetScene = EditorGUILayout.TextField("Target Scene Name", sceneTransition.targetScene);
        }
    }
}
