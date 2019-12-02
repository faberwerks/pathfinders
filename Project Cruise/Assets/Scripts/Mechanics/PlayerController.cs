using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Movement or control of the player to the character
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Joystick joystick;
    private Toggler currentPressurePlate;
    private Toggler currentLever;
    private Interactable interactableObject;
    private Rigidbody2D rb;
    public float speed = 5.0f;
    public Button button;

    // cached variables
    private Vector2 dir;
    private Vector3 translation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        translation = Vector3.zero;
        button = Blackboard.Instance.Button;
        joystick = Blackboard.Instance.Joystick;
    }

    private void FixedUpdate()
    {
        //direction of the character movement from the joystick
        dir = joystick.Direction;
        translation.Set(dir.x, dir.y, translation.z);

        translation = translation.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + translation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.INTERACTABLE_TAG))
        {
            interactableObject = collision.gameObject.GetComponent<Interactable>();
            button.onClick.AddListener(interactableObject.Interact);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.INTERACTABLE_TAG))
        {
            button.onClick.RemoveListener(interactableObject.Interact);
            interactableObject = null;
        }
    }
}
