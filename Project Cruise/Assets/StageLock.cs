using UnityEngine;
using UnityEngine.UI;

public class StageLock : MonoBehaviour
{
    public int firstLevelNumber = 1;
    public Sprite unlockedSprite = null;
    public Sprite lockedSprite = null;

    private Button button = null;
    private Image img = null;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        img = GetComponent<Image>();

        if (GameData.Instance.saveData.LastLevelNumber >= firstLevelNumber)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    private void Lock()
    {
        if (lockedSprite)
        {
            img.sprite = lockedSprite;
        }
        button.interactable = false;
    }

    private void Unlock()
    {
        if (unlockedSprite)
        {
            img.sprite = unlockedSprite;
        }
        button.interactable = true;
    }
}
