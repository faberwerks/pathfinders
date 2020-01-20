using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component to run level button behaviour.
/// </summary>
public class LevelButton : MonoBehaviour
{
    public int levelNumber = 0;
    public Sprite[] levelButtonIcons = new Sprite[4];
    private Image imageComponent = null;

    // Start is called before the first frame update
    private void Start()
    {
        imageComponent = GetComponent<Image>();

        if (GameData.Instance.saveData.levelSaveData.Count >= levelNumber)
        {
            switch (GameData.Instance.saveData.levelSaveData[levelNumber - 1].stars)
            {
                default:
                case 0:
                    imageComponent.sprite = levelButtonIcons[0];
                    break;
                case 1:
                    imageComponent.sprite = levelButtonIcons[1];
                    break;
                case 2:
                    imageComponent.sprite = levelButtonIcons[2];
                    break;
                case 3:
                    imageComponent.sprite = levelButtonIcons[3];
                    break;
            }
        }
    }
}
