using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement or control of the player to the character
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Joystick joystick;
    public float speed = 5.0f;

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
    
    
}
