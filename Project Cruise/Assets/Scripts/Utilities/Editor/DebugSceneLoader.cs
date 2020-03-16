using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DebugSceneLoader : MonoBehaviour
{
    public GameObject debugUI = null;
    public TMP_InputField input = null;

    private void Start()
    {
        if (!Application.isEditor)
        {
            debugUI.SetActive(false);
        }
    }

    public void DebugLoadScene()
    {
        SceneManager.LoadScene(input.text);
    }

    // add menu named "Create New Level" to "Tools" menu
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
}
