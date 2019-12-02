using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctodirectionalJoystick : Joystick
{
    [SerializeField] private float horizontalSensitivity = 22.5f;
    [SerializeField] private float verticalSensitivity = 22.5f;

    protected override void Start()
    {
        base.Start();
        // make snaps fixed
        SnapX = true;
        SnapY = true;
    }

    protected override float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(input, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 0.0f + horizontalSensitivity || angle > 180.0f - horizontalSensitivity)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 90.0f - verticalSensitivity && angle < 90.0f + verticalSensitivity)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            return value;
        }
        else
        {
            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
        }
        return 0;
    }
}
