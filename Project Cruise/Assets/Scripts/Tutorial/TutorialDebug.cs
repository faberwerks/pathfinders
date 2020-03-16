using UnityEngine;
using UnityEngine.UI;

public class TutorialDebug : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = GameData.Instance.tutorialDebug;
    }

    public void ToggleTutorialDebug()
    {
        GameData.Instance.tutorialDebug = toggle.isOn;
    }
}
