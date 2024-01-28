using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimatorManager : MonoBehaviour
{
    public Face face;
    public Animator eyebrow;
    public Animator eyes;
    public Animator mouth;

    private void OnEnable()
    {
        InputHandler.onAbilityTriggered += OnAbilityTriggered;
        InputHandler.onLevelPassed += OnLevelPassed;
    }

    private void OnDisable()
    {
        InputHandler.onAbilityTriggered -= OnAbilityTriggered;
        InputHandler.onLevelPassed -= OnLevelPassed;
    }

    void Update()
    {
        eyebrow.SetFloat("Eyebrow Value", face.eyeBrowsValue);
        eyes.SetFloat("Eye Value", face.eyesValue);
        mouth.SetFloat("Mouth Value", face.mouthValue);
    }

    private void OnLevelPassed()
    {
        eyebrow.SetBool("Win", true);
        eyes.SetBool("Win", true);
        mouth.SetBool("Win", true);
    }

    private void OnAbilityTriggered(Ability ability)
    {
        if (ability.eyebrowsChangeValue > 0) eyebrow.SetTrigger("Eyebrow Feedback");
        if (ability.eyebrowsChangeValue < 0) eyebrow.transform.DOLocalMoveX(0.2f, 0.5f).SetEase(Ease.Flash,10,0.5f);

        if (ability.eyesChangeValue > 0) eyes.SetTrigger("Eye Feedback");
        if (ability.eyesChangeValue < 0) eyes.transform.DOLocalMoveX(0.2f, 0.5f).SetEase(Ease.Flash,10,0.5f);

        if (ability.mouthChangeValue > 0) mouth.SetTrigger("Mouth Feedback");
        if (ability.mouthChangeValue < 0) mouth.transform.DOLocalMoveX(0.2f, 0.5f).SetEase(Ease.Flash,10,0.5f);
        
    }
}
