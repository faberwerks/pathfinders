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
        SceneManager.LoadScene(sceneName);
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
}
