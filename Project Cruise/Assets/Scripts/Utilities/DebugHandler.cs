using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugHandler : MonoBehaviour
{
    public GameObject debugUI = null;
    public TMP_InputField input = null;

    private void Start()
    {
        if (!(Application.isEditor))
        {
            debugUI.SetActive(false);
        }
    }

    public void DebugLoadScene()
    {
        SceneManager.LoadScene(input.text);
    }

    public void DebugUnlockAllLevels()
    {
        while (GameData.Instance.saveData.levelSaveData.Count < 60)
        {
            GameData.Instance.saveData.levelSaveData.Add(new SaveData.LevelSaveData());
        }
        GameData.Instance.saveData.LastLevelNumber = 60;
    }
}
