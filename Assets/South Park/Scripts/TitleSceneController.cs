using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TitleCharacterController))]
public class TitleSceneController : MonoBehaviour {

    public UnityEngine.PostProcessing.PostProcessingProfile PostEffect;
    public Color GlowFirstColor;
    public Color GlowSecondColor;

    private bool _SceneStarted;

    private UnityStandardAssets.ImageEffects.Blur _BlurEffect;
    private GlassShatterEffect _GlassEffect;
    private Animator _SceneAnimator;
    private UnityEngine.PostProcessing.VignetteModel.Settings _VignetteSetting;
    private TitleCharacterController _CharacterScreenController;


    // Use this for initialization
    void Start () {
        _SceneAnimator = GetComponent<Animator>();
        _GlassEffect = GetComponent<GlassShatterEffect>();
        _CharacterScreenController = GetComponent<TitleCharacterController>();
        _BlurEffect = GetComponent<UnityStandardAssets.ImageEffects.Blur>();
        _VignetteSetting = PostEffect.vignette.settings;
        _GlassEffect.EffectMaterial.SetFloat("_GlassThreshold", 0);
        _GlassEffect.EffectMaterial.SetFloat("_GlowThreshold", 0);

        _VignetteSetting.intensity = 0.158f;
    }
	
	// Update is called once per frame
	void Update () {
		if(!_SceneStarted && Input.GetKeyDown(KeyCode.Return))
        {
            _SceneStarted = true;
            StartSceneAnimation();
        }
	}

    void StartSceneAnimation()
    {
        _SceneAnimator.SetTrigger("Start");
    }

    public void AnimateShatterGlass()
    {
        StartCoroutine(BlurIterator());
        DOTween.To(() => _VignetteSetting.intensity, x => _VignetteSetting.intensity = x, 0.256f, 0.5f);
        _GlassEffect.EffectMaterial.DOFloat(1, "_GlassThreshold", 0.5f).OnComplete(StartGlow);
    }


    IEnumerator BlurIterator()
    {
        do
        {
            yield return new WaitForSeconds(0.1f);
            _BlurEffect.iterations++;
        } while (_BlurEffect.iterations < 3);
    }

    void StartGlow()
    {
        _CharacterScreenController.StartAnimateCharacter();
        StartCoroutine(RepeatGlow());
    }

    IEnumerator RepeatGlow()
    {
        _GlassEffect.EffectMaterial.SetColor("_GlowColor", GlowFirstColor);
        yield return new WaitForSeconds(0.2f);
        do
        {
            _GlassEffect.EffectMaterial.DOFloat(1, "_GlowThreshold", 1f).OnComplete(()=> _GlassEffect.EffectMaterial.SetFloat("_GlowThreshold", 0));
            yield return new WaitForSeconds(4f);
            _GlassEffect.EffectMaterial.SetColor("_GlowColor", GlowSecondColor);
        } while (true);
    }
}
