using UnityEngine;

/// <summary>
/// A component to detach an object's children and destroys itself.
/// </summary>
public class ChildrenDetacher : MonoBehaviour
{
    private void Awake()
    {
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
