using UnityEngine;

/// <summary>
/// A component for keys.
/// </summary>
public class Key : MonoBehaviour
{
    [SerializeField]
    private int id = 0;
    public int ID { get { return id; } }
    private int keyAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(keyAmount > 0 &&collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            keyAmount--;
            collision.GetComponentInChildren<KeyHolder>().GetKey(ID);
            Destroy(gameObject);
        }
    }
}
