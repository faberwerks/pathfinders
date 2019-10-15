using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// An editor window to automatically create a level scene.
/// </summary>
public class LevelSceneCreatorWindow : EditorWindow
{
    #region STRING CONSTANTS
    private const string TEXT_FIELD_LABEL = "Level Name";
    private const string HELP_BOX_MESSAGE = "Level name cannot be null or empty!";
    private const string WARNING_BOX_MESSAGE = "A level with the same name already exists!";
    private const string BUTTON_LABEL = "Create Level";
    private const string LEVEL_SETUP_PREFAB_PATH = "Assets/Prefabs/Level/Level Setup.prefab";
    private const string GRID_PREFAB_PATH = "Assets/Prefabs/Level/Level Grid.prefab";
    private const string LEVEL_SETUP_PREFAB_INSTANCE_NAME = "Level Setup";
    private const string GRID_PREFAB_INSTANCE_NAME = "Level Grid";
    private const string SCENE_SAVE_PATH = "Assets/Scenes/Levels/{0}.unity";
    private const string LEVEL_DATA_SAVE_PATH = "Assets/Scenes/Levels/{0}.asset";
    #endregion

    // cached variables
    private GameObject sceneSetupPrefab = null;
    private GameObject sceneGridPrefab = null;
    private string levelName = "";
    private string path = null;
    private bool isNameNullOrEmpty = true;
    private bool levelNameExists = false;

    // add menu named "Create New Level" to "Tools" menu
    [MenuItem("Tools/Create New Level")]
    public static void Init()
    {
        // get existing open window or if none, make new one
        LevelSceneCreatorWindow window = (LevelSceneCreatorWindow)EditorWindow.GetWindow(typeof(LevelSceneCreatorWindow), false, "Level Scene Creator");
        window.Show();
    }

    private void OnGUI()
    {
        levelName = EditorGUILayout.TextField(TEXT_FIELD_LABEL, levelName);

        isNameNullOrEmpty = string.IsNullOrEmpty(levelName);

        if (isNameNullOrEmpty)
        {
            EditorGUILayout.HelpBox(HELP_BOX_MESSAGE, MessageType.Warning);
        }
        else
        {
            // check if level with the same name exists
            if (AssetDatabase.LoadAssetAtPath<Object>(string.Format(SCENE_SAVE_PATH, levelName)) != null)
            {
                levelNameExists = true;
                EditorGUILayout.HelpBox(WARNING_BOX_MESSAGE, MessageType.Error);
            }
            else
            {
                levelNameExists = false;
            }
        }

        EditorGUI.BeginDisabledGroup(levelNameExists);
        if (GUILayout.Button(BUTTON_LABEL))
        {
            if (!isNameNullOrEmpty)
            {
                // checks if scene is dirty and whether user wants to save first
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    // cancels create new level if pop-up to save is cancelled
                    if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        return;
                    }
                }

                // creates new scene with Scene Setup prefab and saves it
                EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

                sceneSetupPrefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>(LEVEL_SETUP_PREFAB_PATH));
                sceneGridPrefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>(GRID_PREFAB_PATH));

                sceneSetupPrefab.name = LEVEL_SETUP_PREFAB_INSTANCE_NAME;
                sceneGridPrefab.name = GRID_PREFAB_INSTANCE_NAME;

                sceneSetupPrefab.transform.localPosition = Vector3.zero;
                sceneSetupPrefab.transform.localEulerAngles = Vector3.zero;
                sceneSetupPrefab.transform.localScale = Vector3.one;

                sceneGridPrefab.transform.localPosition = Vector3.zero;
                sceneGridPrefab.transform.localEulerAngles = Vector3.zero;
                sceneGridPrefab.transform.localScale = Vector3.one;

                path = string.Format(SCENE_SAVE_PATH, levelName);
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);

                // create level data scriptable object
                LevelData asset = ScriptableObject.CreateInstance<LevelData>();
                CreateLevelData();
            }
        }
        EditorGUI.EndDisabledGroup();
    }

    /// <summary>
    /// Creates Level Data for new level.
    /// </summary>
    private void CreateLevelData ()
    {
        LevelData asset = ScriptableObject.CreateInstance<LevelData>();
        asset.levelID = levelName;

        AssetDatabase.CreateAsset(asset, string.Format(LEVEL_DATA_SAVE_PATH, levelName));
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
