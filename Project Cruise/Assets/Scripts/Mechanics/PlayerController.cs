using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Movement or control of the player to the character
/// </summary>
public class PlayerController : MonoBehaviour
{
    private AudioSource walkingSound;
    //private Joystick joystick;
    private MovementArrowManager movementArrowManager;
    private Toggler currentPressurePlate;
    private Toggler currentLever;
    private Interactable interactableObject;
    private Rigidbody2D rb;
    public float speed = 5.0f;
    public Button button;
    private bool isMoving = false;
    private Animator anim = null;
    private bool canMove = true;

    // cached variables
    private Vector2 dir;
    private Vector3 translation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        translation = Vector3.zero;
        button = Blackboard.Instance.Button;
        //joystick = Blackboard.Instance.Joystick;
        movementArrowManager = Blackboard.Instance.movementArrowManager;
        walkingSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        // direction of the character movement from the joystick
        dir = movementArrowManager.Direction;
        translation.Set(dir.x, dir.y, translation.z);
        if (translation != Vector3.zero && !walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
        else if(translation == Vector3.zero)
        {
            walkingSound.Stop();
        }

        // controls animation
        if (translation != Vector3.zero && !isMoving)
        {
            isMoving = true;
            anim.SetTrigger("Move");
        }
        else if (translation == Vector3.zero && isMoving)
        {
            isMoving = false;
            anim.SetTrigger("Stop");
        }

        if (translation.x > 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180.0f, transform.localEulerAngles.z);
        }
        else if (translation.x < 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0.0f, transform.localEulerAngles.z);
        }

        translation = translation.normalized * speed * Time.deltaTime;
        
        if (Blackboard.Instance.LevelManager.CharacterCanMove)
        {
            rb.MovePosition(rb.transform.position + translation);
        }
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
