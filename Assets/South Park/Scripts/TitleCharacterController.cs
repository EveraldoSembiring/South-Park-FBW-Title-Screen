using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCharacterController : MonoBehaviour
{
    public CharacterAnimationControl[] CharacterAnims;

    public Camera CharacterCamera;

    public List<Sprite> UnusedCharacterSprite;

    void Start()
    {
        RenderTexture _Rt = new RenderTexture(Screen.width, Screen.height, 24);
        CharacterCamera.targetTexture = _Rt;
        Shader.SetGlobalTexture("_CharacterTex", _Rt);
        for (int i = 0; i < CharacterAnims.Length; i++)
            CharacterAnims[i].Controller = this;
    }

    public void StartAnimateCharacter()
    {
        for (int i = 0; i < CharacterAnims.Length; i++)
            CharacterAnims[i].StartAnimate();
    }

    public void PickSprite(CharacterAnimationControl charAnim)
    {
        if (UnusedCharacterSprite.Count > 0)
        {
            int indexSprite = 0;
            if (UnusedCharacterSprite.Count > 1)
            {
                indexSprite = Random.Range(0, UnusedCharacterSprite.Count);
            }
            charAnim.CharImage.sprite = UnusedCharacterSprite[indexSprite];
            UnusedCharacterSprite.Remove(UnusedCharacterSprite[indexSprite]);
        }
    }

    public void ReturnSprite(CharacterAnimationControl charAnim)
    {
        if (charAnim.CharImage.sprite != null)
        {
            UnusedCharacterSprite.Add(charAnim.CharImage.sprite);
            charAnim.CharImage.sprite = null;
        }
    }
}
