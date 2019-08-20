using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
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

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadSceneOnKeyDown()
    {
        while (!Input.GetKeyDown(keyToPress))
        {
            yield return null;
        }

        SceneManager.LoadScene(targetScene);
    }
}
