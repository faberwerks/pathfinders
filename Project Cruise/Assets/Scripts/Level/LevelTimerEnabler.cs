using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component to enable level timer only after player interaction.
/// </summary>
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
        if (button) button.onClick.AddListener(OnButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        // check direction in arrow manager
        if (movementManager.Direction != Vector2.zero)
        {
            EnableTimer();
        }
    }

    /// <summary>
    /// Method called at first player interaction in the level.
    /// </summary>
    public void OnButtonPressed()
    {
        button.onClick.RemoveListener(OnButtonPressed);
        EnableTimer();
    }

    /// <summary>
    /// Enables the level timer and destroys this component.
    /// </summary>
    private void EnableTimer()
    {
        timer.EnableTimer();
        Destroy(this);
    }
}
