using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneTransition))]
[CanEditMultipleObjects]
public class SceneTransitionEditor : Editor
{
    private const string clickInfoMessage = "Drag this component to the Button " +
        "component's On Click () field and choose the Load Scene By Name method. Enter the target scene name as the argument.";
    private const string keyDownInfoMessage = "When the key to press is pressed, the target scene will be loaded.";

    public override void OnInspectorGUI()
    {
        SceneTransition sceneTransition = target as SceneTransition;

        sceneTransition.transitionType = (SceneTransition.TransitionType)EditorGUILayout.EnumPopup("Transition Type", sceneTransition.transitionType);

        if (sceneTransition.transitionType == SceneTransition.TransitionType.Click)
        {
            EditorGUILayout.HelpBox(clickInfoMessage, MessageType.Info);
        }

        if (sceneTransition.transitionType == SceneTransition.TransitionType.KeyDown)
        {
            EditorGUILayout.HelpBox(keyDownInfoMessage, MessageType.Info);
            sceneTransition.keyToPress = (KeyCode) EditorGUILayout.EnumPopup("Key To Press", sceneTransition.keyToPress);
            sceneTransition.targetScene = EditorGUILayout.TextField("Target Scene Name", sceneTransition.targetScene);
        }
    }
}
