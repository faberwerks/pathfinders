using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(LevelDirectory))]
public class LevelDirectoryEditor : Editor
{
    #region STRING CONSTANTS
    private const string LEVEL_NUM_LABEL = "Level No.";
    private const string LEVEL_ID_LABEL = "Level ID";
    private const string LEVEL_DIR_LABEL = "Level Directory";
    private const string LEVEL_DIR_PATH = "Level Directory";
    #endregion

    private ReorderableList reorderableList;
    public LevelDirectory levelDir { get { return (target as LevelDirectory); } }

    /// <summary>
    /// A menu item to automatically select the Level Directory and display the inspector.
    /// </summary>
    [MenuItem("Tools/Level Directory")]
    public static void SelectLevelDirectory()
    {
        Selection.activeObject = Resources.Load<LevelDirectory>(LEVEL_DIR_PATH);
    }

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

        GUI.Label(_rect, LEVEL_NUM_LABEL);
        _rect.x += 100;
        GUI.Label(_rect, LEVEL_ID_LABEL);
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

    /// <summary>
    /// Defines what to do when the list is reordered.
    /// </summary>
    /// <param name="list"></param>
    private void OnReorder(ReorderableList list)
    {
        SaveList();
    }

    /// <summary>
    /// Saves the list.
    /// </summary>
    private void SaveList()
    {
        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField(LEVEL_DIR_LABEL);

        EditorGUILayout.Space();

        reorderableList.DoLayoutList();

        SaveList();
    }
}
