using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class DebugStarter
{
    // add menu named "Start" to "Tools" menu
    [MenuItem("Tools/Start")]
    public static void Init()
    {
        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            // cancels start if pop-up to save is cancelled
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                return;
            }
        }

        EditorSceneManager.OpenScene("Assets/Scenes/Loading.unity");
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    // [MenuItem("Tools/List Scene Names")]
    public static void LogNames()
    {
        List<string> temp = new List<string>();
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        for (int i = 7; i < 27; i++)
        {
                string name = scenes[i].path.Substring(scenes[i].path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);

                EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

                string path = string.Format("Assets/Scenes/Levels/NEW/{0}.unity", name);
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);
        }
    }
}
