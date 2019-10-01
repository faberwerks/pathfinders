using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickArrowHandler : MonoBehaviour
{
    OctodirectionalJoystick joystick;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        joystick = GetComponentInParent<OctodirectionalJoystick>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowChange();
    }

    private void ArrowChange()
    {
        if (joystick.Horizontal == 0 && joystick.Vertical == 0 && image.enabled)
        {
            image.enabled = !image.enabled;
        }
        else if(joystick.Horizontal != 0 && joystick.Vertical != 0 && !image.enabled)
        {
            image.enabled = !image.enabled;
        }
        //if (joystick.Horizontal == 0 && joystick.Vertical == 1)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //}
        //else if (joystick.Horizontal == -1 && joystick.Vertical == 1)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 45)));
        //}
        //else if (joystick.Horizontal == -1 && joystick.Vertical == 0)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        //}
        //else if (joystick.Horizontal == -1 && joystick.Vertical == -1)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 135)));
        //}
        //else if (joystick.Horizontal == 0 && joystick.Vertical == -1)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        //}
        //else if (joystick.Horizontal == 1 && joystick.Vertical == -1)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 225)));
        //}
        //else if (joystick.Horizontal == 1 && joystick.Vertical == 0)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
        //}
        //else if (joystick.Horizontal == 1 && joystick.Vertical == 1)
        //{
        //    transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 315)));
        //}

        if(joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            Debug.Log(Mathf.Atan2(joystick.Vertical, joystick.Horizontal));
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2( joystick.Vertical,joystick.Horizontal)*180/Mathf.PI))));
        }
    }
}
