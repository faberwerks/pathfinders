using UnityEngine;
using UnityEngine.UI;

public class LevelTimerEnabler : MonoBehaviour
{
    public MovementArrowManager movementManager;

    private LevelTimer timer;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.GetComponent<LevelTimer>();
        button = Blackboard.Instance.Button;
        if (button) button.onClick.AddListener(ButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        //check Direction in arrow manager
        if (movementManager.Direction != Vector2.zero)
        {
            //Debug.Log("Arrow Pressed");
            EnableTimer();
        }
    }

    public void ButtonPressed()
    {
        //Debug.Log("Button Pressed");
        button.onClick.RemoveListener(ButtonPressed);
        EnableTimer();
    }

    private void EnableTimer()
    {
        timer.EnableTimer();
        Destroy(this);
    }
}
