using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Movement or control of the player to the character
/// </summary>
public class PlayerController : MonoBehaviour
{
    private GameObject gate;
    private Joystick joystick;
    private Toggler currentPressurePlate;
    private Toggler currentLever;
    private Interactable interactableObject;
    private Rigidbody2D rb;
    public float speed = 5.0f;
    public Button button;
    public bool hasKey;

    // cached variables
    private Vector2 dir;
    private Vector3 translation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        translation = Vector3.zero;
        hasKey = false;
        button = Blackboard.instance.Button;
    }

    // Update is called once per frame
    private void Update()
    {
        if(joystick == null)
        {
            joystick = Blackboard.instance.Joystick;
        }
        if(button == null)
        {
            button = Blackboard.instance.Button;
        }

        //direction of the character movement from the joystick
        //dir = joystick.Direction;
        //translation.Set(dir.x, dir.y, translation.z);

        //transform.Translate(translation * speed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        //direction of the character movement from the joystick
        dir = joystick.Direction;
        translation.Set(dir.x, dir.y, translation.z);

        translation = translation.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + translation);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(TagStrings.OBSTACLE_TAG) && gate == collision.gameObject)
        {
            button.onClick.AddListener(() => collision.gameObject.SetActive(false));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagStrings.OBSTACLE_TAG))
        {
            button.onClick.RemoveListener(() => collision.gameObject.SetActive(false));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Lever"))
        //{
        //    currentLever = collision.gameObject.GetComponent<Toggler>();
        //    button.onClick.AddListener(currentLever.ToggleObjects);
        //}

        //if (collision.CompareTag("PressurePlate")){
        //    currentPressurePlate = collision.gameObject.GetComponent<Toggler>();
        //    currentPressurePlate.ToggleObjects();
        //}

        if (collision.CompareTag(TagStrings.INTERACTABLE_TAG))
        {
            interactableObject = collision.gameObject.GetComponent<Interactable>();
            button.onClick.AddListener(interactableObject.Interact);
        }
        
        if (collision.CompareTag(TagStrings.KEY_TAG) && !hasKey)
        {
            gate = collision.gameObject.GetComponent<KeyMapping>().gate;
            hasKey = true;
            Destroy(collision.gameObject);
            Debug.Log(hasKey);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.CompareTag("Lever"))
        //{
        //    button.onClick.RemoveListener(currentLever.ToggleObjects);
        //    currentLever = null;
        //}
        //if (collision.CompareTag("PressurePlate"))
        //{
        //    currentPressurePlate.ToggleObjects();
        //    currentPressurePlate = null;
        //}

        if (collision.CompareTag(TagStrings.INTERACTABLE_TAG))
        {
            button.onClick.RemoveListener(interactableObject.Interact);
            interactableObject = null;
        }
    }
}
