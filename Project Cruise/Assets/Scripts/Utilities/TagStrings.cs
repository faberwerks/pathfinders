using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class TagStrings
{
    private static TagStrings instance = null;

    public static TagStrings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TagStrings();
            }
            return instance;
        }
    }

    private TagStrings()
    {
        
    }
}
