using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to assign the InteractButton to the Blackboard.
/// </summary>
public class InteractButton : MonoBehaviour
{
    private void Start()
    {
        Blackboard.instance.Button = gameObject.GetComponent<Button>();
    }
}
