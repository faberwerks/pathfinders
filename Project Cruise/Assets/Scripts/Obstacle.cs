using UnityEngine;

/// <summary>
/// A component handling Obstacle behaviour.
/// </summary>
public class Obstacle : MonoBehaviour
{
    private PlayerController player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagStrings.PLAYER_TAG))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    //private void OnDestroy()
    //{
    //    if (player != null)
    //    {
    //        player.hasKey = false;
    //    }
    //}
}
