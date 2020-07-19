using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A component to handle scene transitions generically.
/// </summary>
public class SceneTransition : MonoBehaviour
{
    // CLICK - use for UI buttons
    // KEYDOWN - use for keys such as Escape
    public enum TransitionType { Click, KeyDown };

    public TransitionType transitionType = TransitionType.Click;
    public KeyCode keyToPress = KeyCode.Escape;
    public string targetScene = "";

    // Start is called before the first frame update
    private void Start()
    {
        if (transitionType == TransitionType.KeyDown)
        {
            StartCoroutine(LoadSceneOnKeyDown());
        }
    }

    /// <summary>
    /// Loads a specific scene by name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to be loaded.</param>
    public void LoadSceneByName(string sceneName)
    {
        GameData.Instance.previousSceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Reloads the open scene.
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads previous level scene that was played.
    /// </summary>
    public void LoadPreviousLevelScene()
    {
        SceneManager.LoadScene(LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID).levelID);
    }

    /// <summary>
    /// Loads the next level scene.
    /// </summary>
    public void LoadNextLevelScene()
    {
        // TEMPORARY
        // if (GameData.Instance.lastSceneBuildIndex == 18) SceneManager.LoadScene("MainMenu");
        GameData.Instance.currLevelID++;
        //Debug.Log("NEXT LEVEL: " + LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID).levelID);
        SceneManager.LoadScene(LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID).levelID);
        
    }

    /// <summary>
    /// Loads a scene when the specified key is pressed.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneOnKeyDown()
    {
        while (!Input.GetKeyDown(keyToPress))
        {
            yield return null;
        }

        SceneManager.LoadScene(targetScene);
    }

    /// <summary>
    /// Loads a level with the specified index.
    /// </summary>
    /// <param name="index">Level index.</param>
    public void LoadLevel(int index)
    {
        GameData.Instance.currLevelID = index;
        SceneManager.LoadScene(LevelDirectory.Instance.GetLevelData(index).levelID);
        
    }
}
