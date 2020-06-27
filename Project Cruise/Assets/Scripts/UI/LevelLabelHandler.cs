using UnityEngine;
using TMPro;

public class LevelLabelHandler : MonoBehaviour
{
    private TMP_Text levelLabel;

    // Start is called before the first frame update
    private void Start()
    {
        levelLabel = GetComponent<TMP_Text>();

        levelLabel.text = "Level " + GameData.Instance.currLevelID;
    }
}
