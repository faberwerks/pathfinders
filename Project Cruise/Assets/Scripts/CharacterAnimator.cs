using UnityEngine;

/// <summary>
/// Component to change Character type.
/// </summary>
public class CharacterAnimator : MonoBehaviour
{
    public enum CharacterType
    {
        Jones,
        Guard,
        Archaeologist
    }

    public RuntimeAnimatorController jones;
    public Sprite jonesSprite;
    public RuntimeAnimatorController guard;
    public Sprite guardSprite;
    public RuntimeAnimatorController archaeologist;
    public Sprite archaeologistSprite;

    public CharacterType characterType;

    private Animator anim;
    private SpriteRenderer sprRend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();

        RuntimeAnimatorController animController = jones;
        Sprite spr = jonesSprite;

        switch (characterType)
        {
            case CharacterType.Jones:
                animController = jones;
                spr = jonesSprite;
                break;
            case CharacterType.Guard:
                animController = guard;
                spr = guardSprite;
                break;
            case CharacterType.Archaeologist:
                animController = archaeologist;
                spr = archaeologistSprite;
                break;
        }

        anim.runtimeAnimatorController = animController;
        sprRend.sprite = spr;

    }
}
