using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OctodirectionalJoystick), true)]
public class OctodirectionalJoystickEditor : Editor
{
    private SerializedProperty handleRange;
    private SerializedProperty deadZone;
    private SerializedProperty axisOptions;
    protected SerializedProperty background;
    private SerializedProperty handle;
    private SerializedProperty horizontalSensitivty;
    private SerializedProperty verticalSensitivity;

    protected Vector2 center = new Vector2(0.5f, 0.5f);

    protected virtual void OnEnable()
    {
        handleRange = serializedObject.FindProperty("handleRange");
        deadZone = serializedObject.FindProperty("deadZone");
        axisOptions = serializedObject.FindProperty("axisOptions");
        background = serializedObject.FindProperty("background");
        handle = serializedObject.FindProperty("handle");
        horizontalSensitivty = serializedObject.FindProperty("horizontalSensitivity");
        verticalSensitivity = serializedObject.FindProperty("verticalSensitivity");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawValues();
        EditorGUILayout.Space();
        DrawComponents();

        serializedObject.ApplyModifiedProperties();

        if (handle != null)
        {
            RectTransform handleRect = (RectTransform)handle.objectReferenceValue;
            handleRect.anchorMax = center;
            handleRect.anchorMin = center;
            handleRect.pivot = center;
            handleRect.anchoredPosition = Vector2.zero;
        }
    }

    protected virtual void DrawValues()
    {
        EditorGUILayout.PropertyField(handleRange, new GUIContent("Handle Range", "The distance the visual handle can move from the center of the joystick."));
        EditorGUILayout.PropertyField(deadZone, new GUIContent("Dead Zone", "The distance away from the center input has to be before registering."));
        EditorGUILayout.PropertyField(axisOptions, new GUIContent("Axis Options", "Which axes the joystick uses."));
        EditorGUILayout.PropertyField(horizontalSensitivty, new GUIContent("Horizontal Sensitivity", "Horizontal sensitivity."));
        EditorGUILayout.PropertyField(verticalSensitivity, new GUIContent("Vertical Sensitivity", "Vertical sensitivity."));
    }

    protected virtual void DrawComponents()
    {
        EditorGUILayout.ObjectField(background, new GUIContent("Background", "The background's RectTransform component."));
        EditorGUILayout.ObjectField(handle, new GUIContent("Handle", "The handle's RectTransform component."));
    }
}