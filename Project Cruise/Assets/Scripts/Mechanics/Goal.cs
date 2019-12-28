using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to define what the goal must do 
/// </summary>
public class Goal : MonoBehaviour
{
    /////// PROPERTIES ///////
    public bool IsPressed { get; set; }
    public Sprite[] goalSprites = new Sprite[2];

    private AudioSource stepOnGoal;
    private SpriteRenderer sprRend;

    // Start is called before the first frame update
    void Start()
    {
        IsPressed = false;
        Blackboard.Instance.LevelManager.goals.Add(this);
        //Blackboard.instance.LevelManager.goals.Add(this);
        stepOnGoal = GetComponent<AudioSource>();
        sprRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!Blackboard.instance.LevelManager.goals.Contains(this))
        //{
        //    Blackboard.instance.LevelManager.goals.Add(this);
        //}
    }

    //Method CheckGoals is called here so there is no need for this method called in update function 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            IsPressed = true;
            sprRend.sprite = goalSprites[1];
            stepOnGoal.Play();
            Blackboard.Instance.LevelManager.CheckGoals();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            IsPressed = false;
            sprRend.sprite = goalSprites[0];
        }
    }
}
