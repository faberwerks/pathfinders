using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugHandler : MonoBehaviour
{
    public GameObject debugUI = null;
    public TMP_InputField input = null;
    public int[] relicLevels;

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

    public void DebugFoundAllRelics()
    {
        for (int i = 0; i < 5; i++)
        {
            GameData.Instance.saveData.levelSaveData[relicLevels[i] - 1].hasFoundRelic = true;
        }
    }

    public void DebugRemoveAllRelics()
    {
        for (int i = 0; i < 5; i++)
        {
            GameData.Instance.saveData.levelSaveData[relicLevels[i] - 1].hasFoundRelic = false;
        }
    }

    public void DebugUnlockMostLevelsWithThreeStars()
    {
        DebugUnlockAllLevels();
        for (int i = 0; i < GameData.Instance.saveData.levelSaveData.Count; i++)
        {
            if (i == 3)
            {
                continue;
            }
            else
            {
                GameData.Instance.saveData.levelSaveData[i].hasAchievedThreeStars = true;
            }
        }
    }
}
