using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(LevelDirectory))]
public class LevelDirectoryEditor : Editor
{
    #region CONST STRINGS
    private const string levelNumberLabel = "Level No.";
    private const string levelIDLabel = "Level ID";
    private const string levelDirectoryLabel = "Level Directory";
    #endregion

    private ReorderableList reorderableList;
    public LevelDirectory levelDir { get { return (target as LevelDirectory); } }

    private void OnEnable()
    {
        reorderableList = new ReorderableList(levelDir.levels, typeof(LevelData), true, true, true, true);
        reorderableList.drawHeaderCallback = OnDrawHeader;
        reorderableList.drawElementCallback = OnDrawElement;
        reorderableList.onAddCallback = OnAdd;
        reorderableList.onReorderCallback = OnReorder;
    }

    /// <summary>
    /// Draws the header for the Level Directory reorderable list.
    /// </summary>
    /// <param name="rect"></param>
    private void OnDrawHeader(Rect rect)
    {
        Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

        GUI.Label(_rect, levelNumberLabel);
        _rect.x += 100;
        GUI.Label(_rect, levelIDLabel);
    }

    /// <summary>
    /// A method to determine what to draw for each element on the list.
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="index"></param>
    /// <param name="isActive"></param>
    /// <param name="isFocused"></param>
    private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        rect.y += 2;
        Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

        // display index
        EditorGUI.LabelField(_rect, (index + 1).ToString());
        _rect.x += 100;
        _rect.width = 150;
        // display level data asset
        levelDir.levels[index] = (LevelData) EditorGUI.ObjectField(_rect, levelDir.levels[index], typeof(LevelData), false);
    }

    /// <summary>
    /// Defines how a new Level Data asset should be added to the list.
    /// </summary>
    /// <param name="list"></param>
    private void OnAdd(ReorderableList list)
    {
        levelDir.levels.Add(null);
    }

    private void OnReorder(ReorderableList list)
    {
        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField(levelDirectoryLabel);

        EditorGUILayout.Space();

        reorderableList.DoLayoutList();

        EditorUtility.SetDirty(target);
    }
}
