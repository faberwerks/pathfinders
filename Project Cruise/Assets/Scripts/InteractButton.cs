using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractButton : Button
{
    private new void Start()
    {
        Blackboard.instance.Button = this;
    }
}
