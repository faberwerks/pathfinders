using UnityEngine;

/// <summary>
/// A component for keys.
/// </summary>
public class Key : MonoBehaviour
{
    [SerializeField]
    private int id = 0;
    public int ID { get { return id; } }
}
