using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : Button
{
    private new void Start()
    {
        Blackboard.instance.button = this;
    }
}
