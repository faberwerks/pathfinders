using System.Collections;
using System.Collections.Generic;
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
    public float speed = 5.0f;
    public Button button;

    // cached variables
    private Vector2 dir;
    private Vector3 translation;

    // Start is called before the first frame update
    void Start()
    {
        translation = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if(joystick == null)
        {
            joystick = Blackboard.instance.Joystick;
        }

        //direction of the character movement from the joystick
        dir = joystick.Direction;
        translation.Set(dir.x, dir.y, translation.z);

        transform.Translate(translation * speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lever"))
        {
            currentLever = collision.gameObject.GetComponent<Toggler>();
            button.onClick.AddListener(currentLever.ToggleObjects);
        }

        if (collision.CompareTag("PressurePlate")){
            currentPressurePlate = collision.gameObject.GetComponent<Toggler>();
            currentPressurePlate.ToggleObjects();
        }

        if (collision.CompareTag("Interactable"))
        {
            interactableObject = collision.gameObject.GetComponent<Interactable>();
            button.onClick.AddListener(interactableObject.Interact);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lever"))
        {
            button.onClick.RemoveListener(currentLever.ToggleObjects);
            currentLever = null;
        }
        if (collision.CompareTag("PressurePlate"))
        {
            currentPressurePlate.ToggleObjects();
            currentPressurePlate = null;
        }

        if (collision.CompareTag("Interactable"))
        {
            button.onClick.RemoveListener(interactableObject.Interact);
            interactableObject = null;
        }
    }
}
