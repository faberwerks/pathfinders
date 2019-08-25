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
    private const string BUTTON_LABEL = "Create Level";
    private const string PREFAB_PATH = "Assets/Prefabs/Level Setup.prefab";
    private const string PREFAB_INSTANCE_NAME = "Level Setup";
    private const string SCENE_SAVE_PATH = "Assets/Scenes/Levels/{0}.unity";
    #endregion

    // cached variables
    private GameObject sceneSetupPrefab = null;
    private string levelName = "";
    private string path = null;
    private bool isNameNullOrEmpty = true;

    // add menu named "Level Scene Creator" to "Tools" menu
    [MenuItem("Tools/Create New Level")]
    public static void Init()
    {
        // get existing open window or if none, make new one
        LevelSceneCreatorWindow window = (LevelSceneCreatorWindow)EditorWindow.GetWindow(typeof(LevelSceneCreatorWindow));
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

                sceneSetupPrefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>(PREFAB_PATH));

                sceneSetupPrefab.name = PREFAB_INSTANCE_NAME;

                sceneSetupPrefab.transform.localPosition = Vector3.zero;
                sceneSetupPrefab.transform.localEulerAngles = Vector3.zero;
                sceneSetupPrefab.transform.localScale = Vector3.one;

                path = string.Format(SCENE_SAVE_PATH, levelName);
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);
            }
        }
    }
}
