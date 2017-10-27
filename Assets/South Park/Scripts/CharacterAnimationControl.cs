using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour {
    [HideInInspector]
    public TitleCharacterController Controller;

    public UnityEngine.UI.Image CharImage;
    public bool IsMoveRight;
    public Vector2 CharacterPopUpTimeRange = new Vector2(2f, 10f);

    private Animator _CharacterAnimator;

    private void Start()
    {
        _CharacterAnimator = GetComponent<Animator>();
    }

    public void StartAnimate()
    {
        Controller.ReturnSprite(this);
        Controller.PickSprite(this);
        StartCoroutine(AnimateCharacter());
    }

    IEnumerator AnimateCharacter()
    {
        yield return new WaitForSeconds(Random.Range(CharacterPopUpTimeRange.x, CharacterPopUpTimeRange.y));
        if(IsMoveRight)
            _CharacterAnimator.SetTrigger("AnimateRight");
        else
            _CharacterAnimator.SetTrigger("AnimateLeft");
    }
}
