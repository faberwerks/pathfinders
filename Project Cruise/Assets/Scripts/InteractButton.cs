using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to assign the InteractButton to the Blackboard.
/// </summary>
public class InteractButton : MonoBehaviour
{
    private void Awake()
    {
        Blackboard.Instance.Button = gameObject.GetComponent<Button>();
    }
}
