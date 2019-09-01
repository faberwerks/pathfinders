using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagStrings.PLAYER_TAG))
        {
            Blackboard.instance.LevelManager.Lose();
        }
    }
}
