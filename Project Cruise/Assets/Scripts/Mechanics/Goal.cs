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
    private GameObject playerOn;

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
        playerOn = null;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(TagStrings.PLAYER_TAG) && playerOn == null && CheckPlayer(collision.gameObject))
        {
            IsPressed = true;
            sprRend.sprite = goalSprites[1];
            Blackboard.Instance.LevelManager.CheckGoals();
            playerOn = collision.gameObject;
            Blackboard.Instance.LevelManager.playerOnGoal.Add(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG) && playerOn == null && CheckPlayer(collision.gameObject))
        {
            stepOnGoal.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG) && collision.gameObject == playerOn)
        {
            IsPressed = false;
            sprRend.sprite = goalSprites[0];
            playerOn = null;
            Blackboard.Instance.LevelManager.playerOnGoal.Remove(collision.gameObject);
        }
    }

    private bool CheckPlayer(GameObject player)
    {
        foreach (GameObject gameobject in Blackboard.Instance.LevelManager.playerOnGoal)
        {
            if (player == gameobject)
            {
                return false;
            }
        }
        return true;
    }
}
