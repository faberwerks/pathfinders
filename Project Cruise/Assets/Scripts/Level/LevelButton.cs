using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Component to run level button behaviour.
/// </summary>
public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public Sprite[] levelButtonIcons = new Sprite[4];
    private Image imageComponent = null;

    // Start is called before the first frame update
    private void Start()
    {
        imageComponent = GetComponent<Image>();
        var button = GetComponent<Button>();
        var sceneTransition = GetComponent<SceneTransition>();
        var text = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        button.interactable = false;
        button.onClick.AddListener(() => sceneTransition.LoadLevel(levelNumber));

        text.text = levelNumber.ToString();

        if (GameData.Instance.saveData.levelSaveData.Count >= levelNumber)
        {
            button.interactable = true;
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
