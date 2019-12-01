using UnityEngine;

public class Trap : MonoBehaviour
{
    public AudioSource hitOnTrap;
    public AudioSource die;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagStrings.PLAYER_TAG))
        {
            hitOnTrap.Play();
            die.Play();
            Blackboard.Instance.LevelManager.Lose();
        }
    }
}
