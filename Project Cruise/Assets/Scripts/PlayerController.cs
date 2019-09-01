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
    private Button button;
    public bool hasKey;
    public float speed = 5.0f;


    // cached variables
    private Vector2 dir;
    private Vector3 translation;


    // Start is called before the first frame update
    void Start()
    {
        translation = Vector3.zero;
        hasKey = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(joystick == null)
        {
            joystick = Blackboard.instance.Joystick;
            button = Blackboard.instance.Button;
        }

        //direction of the character movement from the joystick
        dir = joystick.Direction;
        translation.Set(dir.x, dir.y, translation.z);

        transform.Translate(translation * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle") && gate == collision.gameObject)
        {
            button.onClick.AddListener(() => collision.gameObject.SetActive(false));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            button.onClick.RemoveListener(() => collision.gameObject.SetActive(false));
        }
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

        if (collision.CompareTag("Key") && !hasKey)
        {
            gate = collision.gameObject.GetComponent<KeyMapping>().gate;
            hasKey = true;
            Destroy(collision.gameObject);
            Debug.Log(hasKey);
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
    }
}
