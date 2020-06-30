using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicShowcaseHandler : MonoBehaviour
{
    //relic levels list
    public int[] levels;
    //ref to Relic images
    public Image[] relics;
    //ref to TMPs
    public TextMeshProUGUI[] levelTexts;

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

                //check hasFoundRelic in specific levelSaveData, if TRUE setActive the image, mark hasFoundRelic as true
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
