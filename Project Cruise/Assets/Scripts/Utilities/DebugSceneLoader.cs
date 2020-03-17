using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
