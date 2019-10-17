using System;
using System.Collections.Generic;
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
    private readonly string[] LEVEL_TYPE_OPTIONS =
    {
        "Movement",
        "Trap",
        "Lever",
        "Key",
        "Pressure Plate",
        "Unproportional Level",
        "Trigger Timer",
        "Teleporter"
    };
    #endregion

    // cached variables
    private GameObject sceneSetupPrefab = null;
    private GameObject sceneGridPrefab = null;
    private string levelName = "";
    private string path = null;
    //private bool isNameNullOrEmpty = true;
    //private bool levelNameExists = false;
    private bool canCreateLevel = true;
    private int levelTypeIndex = 0;
    private int levelIndex = 0;

    // add menu named "Create New Level" to "Tools" menu
    [MenuItem("Tools/Create New Level")]
    public static void Init()
    {
        // get existing open window or if none, make new one
        LevelSceneCreatorWindow window = (LevelSceneCreatorWindow)EditorWindow.GetWindow(typeof(LevelSceneCreatorWindow), false, "Level Scene Creator");
        window.Show();
    }

    private void OnEnable()
    {
        levelIndex = LevelDirectory.Instance.levels.Count + 1;
    }

    private void OnGUI()
    {
        //levelName = EditorGUILayout.TextField(TEXT_FIELD_LABEL, levelName);

        levelTypeIndex = EditorGUILayout.Popup("Level Type", levelTypeIndex, LEVEL_TYPE_OPTIONS);
        levelIndex = EditorGUILayout.IntField("Level Index", levelIndex);

        if (levelIndex > LevelDirectory.Instance.levels.Count + 1 || levelIndex < 1)
        {
            canCreateLevel = false;
        }
        else
        {
            canCreateLevel = true;
        }

        //isNameNullOrEmpty = string.IsNullOrEmpty(levelName);

        //if (isNameNullOrEmpty)
        //{
        //    EditorGUILayout.HelpBox(HELP_BOX_MESSAGE, MessageType.Warning);
        //}
        //else
        //{
        //    // check if level with the same name exists
        //    if (AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(string.Format(SCENE_SAVE_PATH, levelName)) != null)
        //    {
        //        levelNameExists = true;
        //        EditorGUILayout.HelpBox(WARNING_BOX_MESSAGE, MessageType.Error);
        //    }
        //    else
        //    {
        //        levelNameExists = false;
        //    }
        //}

        EditorGUI.BeginDisabledGroup(!canCreateLevel);
        if (GUILayout.Button(BUTTON_LABEL))
        {
            //if (!isNameNullOrEmpty)
            //{
            //    // checks if scene is dirty and whether user wants to save first
            //    if (EditorSceneManager.GetActiveScene().isDirty)
            //    {
            //        // cancels create new level if pop-up to save is cancelled
            //        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            //        {
            //            return;
            //        }
            //    }

            //    // creates new scene with Scene Setup prefab and saves it
            //    EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            //    sceneSetupPrefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(LEVEL_SETUP_PREFAB_PATH));
            //    sceneGridPrefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(GRID_PREFAB_PATH));

            //    sceneSetupPrefab.name = LEVEL_SETUP_PREFAB_INSTANCE_NAME;
            //    sceneGridPrefab.name = GRID_PREFAB_INSTANCE_NAME;

            //    sceneSetupPrefab.transform.localPosition = Vector3.zero;
            //    sceneSetupPrefab.transform.localEulerAngles = Vector3.zero;
            //    sceneSetupPrefab.transform.localScale = Vector3.one;

            //    sceneGridPrefab.transform.localPosition = Vector3.zero;
            //    sceneGridPrefab.transform.localEulerAngles = Vector3.zero;
            //    sceneGridPrefab.transform.localScale = Vector3.one;

            //    path = string.Format(SCENE_SAVE_PATH, levelName);
            //    EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);

            //    // create level data scriptable object
            //    CreateLevelData();
            //}

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

            sceneSetupPrefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(LEVEL_SETUP_PREFAB_PATH));
            sceneGridPrefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(GRID_PREFAB_PATH));

            sceneSetupPrefab.name = LEVEL_SETUP_PREFAB_INSTANCE_NAME;
            sceneGridPrefab.name = GRID_PREFAB_INSTANCE_NAME;

            sceneSetupPrefab.transform.localPosition = Vector3.zero;
            sceneSetupPrefab.transform.localEulerAngles = Vector3.zero;
            sceneSetupPrefab.transform.localScale = Vector3.one;

            sceneGridPrefab.transform.localPosition = Vector3.zero;
            sceneGridPrefab.transform.localEulerAngles = Vector3.zero;
            sceneGridPrefab.transform.localScale = Vector3.one;

            levelName = GenerateLevelName(levelTypeIndex);
            path = string.Format(SCENE_SAVE_PATH, levelName);
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);

            // create level data scriptable object
            CreateLevelData(levelName, levelIndex);
        }
        EditorGUI.EndDisabledGroup();
    }

    /// <summary>
    /// Creates Level Data for new level.
    /// </summary>
    private void CreateLevelData (string levelName, int index)
    {
        LevelData asset = ScriptableObject.CreateInstance<LevelData>();
        asset.levelID = levelName;

        AssetDatabase.CreateAsset(asset, string.Format(LEVEL_DATA_SAVE_PATH, levelName));
        if (index == LevelDirectory.Instance.levels.Count + 1)
        {
            LevelDirectory.Instance.levels.Add(asset);
        }
        else
        {
            LevelDirectory.Instance.levels.Insert(index - 1, asset);
        }
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    /// <summary>
    /// A method to generate the level name.
    /// </summary>
    /// <param name="levelTypeIndex"></param>
    /// <returns></returns>
    private string GenerateLevelName(int levelTypeIndex)
    {
        string levelName = "";
        int levelNum = 1;

        switch (levelTypeIndex)
        {
            case 0:
                levelName = "M";
                break;
            case 1:
                levelName = "T";
                break;
            case 2:
                levelName = "L";
                break;
            case 3:
                levelName = "K";
                break;
            case 4:
                levelName = "P";
                break;
            case 5:
                levelName = "U";
                break;
            case 6:
                levelName = "TR";
                break;
            case 7:
                levelName = "TL";
                break;
        }

        List<string> levelNames = new List<string>();
        string substring = "";
        int tempLevelNum = 0;
        int highestLevelNum = 0;

        foreach (LevelData levelData in LevelDirectory.Instance.levels)
        {
            if (levelData.levelID.StartsWith(levelName))
            {
                substring = levelData.levelID.Substring((levelData.levelID.Length - 1) - 1);
                tempLevelNum = Int32.Parse(substring);

                if (tempLevelNum >= highestLevelNum)
                {
                    highestLevelNum = tempLevelNum;
                }
            }
        }

        if (highestLevelNum > 0)
        {
            levelNum = highestLevelNum + 1;
        }
        else
        {
            levelNum = 1;
        }

        string levelNumString = "";

        if (levelNum < 10)
        {
            levelNumString = levelNum.ToString();
            levelNumString = String.Concat("0", levelNumString);
        }
        else
        {
            levelNumString = levelNum.ToString();
        }

        levelName = String.Concat(levelName, levelNumString);

        return levelName;
    }
}
