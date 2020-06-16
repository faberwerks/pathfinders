using UnityEngine;

public class Trap : MonoBehaviour
{
    public AudioClip hitSound;

    private AudioHandler audioHandler;

    public static int totalCollide;

    private void Start()
    {
        audioHandler = GetComponent<AudioHandler>();
        ResetTotalCollide();
    }

    private void ResetTotalCollide()
    {
        Debug.Log("reset");
        totalCollide = 0;
    }

    void Update()
    {
        if (totalCollide > 0)
        {
            ResetTotalCollide();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagStrings.PLAYER_TAG) && totalCollide < 1)
        {
            Debug.Log("Die");
            audioHandler.Play(hitSound);
            totalCollide += 1;
            collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
            Blackboard.Instance.LevelManager.DisableCharacterMovement();
            Blackboard.Instance.LevelManager.EndLevelTimer();
        }
    }
}
