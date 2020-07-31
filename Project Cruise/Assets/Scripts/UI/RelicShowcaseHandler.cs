using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component to handle the relic showcase UI.
/// </summary>
public class RelicShowcaseHandler : MonoBehaviour
{
    public int[] levels;    // relic levels list
    public Image[] relics;  // refs to relic images
    public TextMeshProUGUI[] levelTexts;    // refs to TMPs

    public Image panelButton;

    private bool hasFoundRelic = false;

    // Start is called before the first frame update
    void Start()
    {
        int lastLevelNumber = GameData.Instance.saveData.LastLevelNumber;
        for (int i = 0; i < 5; i++)
        {
            if (lastLevelNumber >= levels[i]) {
                levelTexts[i].text = levels[i].ToString();

                // check hasFoundRelic in specific levelSaveData, if TRUE setActive the image, mark hasFoundRelic as true
                if (GameData.Instance.saveData.levelSaveData[levels[i] - 1].hasFoundRelic)
                {
                    relics[i].gameObject.SetActive(true);
                    hasFoundRelic = true;
                }
            }
        }

        if (hasFoundRelic)
        {
            panelButton.gameObject.SetActive(true);
        }
    }
}
