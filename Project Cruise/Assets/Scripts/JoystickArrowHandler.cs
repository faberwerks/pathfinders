using UnityEngine;
using UnityEngine.UI;

public class JoystickArrowHandler : MonoBehaviour
{
    private OctodirectionalJoystick joystick;
    private Image image;

    // Start is called before the first frame update
    private void Start()
    {
        joystick = GetComponentInParent<OctodirectionalJoystick>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        DrawArrow();
    }

    /// <summary>
    /// A method to draw the movement arrows.
    /// </summary>
    private void DrawArrow()
    {
        if (joystick.Horizontal == 0 && joystick.Vertical == 0 && image.enabled)
        {
            image.enabled = !image.enabled;
        }
        else if ((joystick.Horizontal != 0 || joystick.Vertical != 0) && !image.enabled)
        {
            image.enabled = !image.enabled;
        }

        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, (Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * 180 / Mathf.PI - 90)));
        }
        else
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, ((joystick.Vertical < 0 ? 180f : 0) + -joystick.Horizontal * 90)));
        }
    }
}
