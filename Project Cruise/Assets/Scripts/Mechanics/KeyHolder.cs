using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component to hold key-door ID.
/// </summary>
public class KeyHolder : MonoBehaviour
{
    private int id;
    private Button button;
    public GameObject keySprite;

    // Start is called before the first frame update
    private void Start()
    {
        id = 0;
        button = Blackboard.Instance.Button;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (id == 0 && collision.CompareTag(TagStrings.KEY_TAG))
        {
            id = collision.GetComponent<Key>().ID;
            keySprite.SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag(TagStrings.DOOR_TAG) && id == collision.GetComponent<Door>().ID)
        {
            collision.GetComponent<Door>().Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.DOOR_TAG))
        {
            button.onClick.RemoveListener(collision.GetComponent<Door>().Interact);
        }
    }
}
