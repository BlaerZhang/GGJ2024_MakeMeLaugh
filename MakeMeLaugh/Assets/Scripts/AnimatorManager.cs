using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimatorManager : MonoBehaviour
{
    private InputHandler inputHandler;
    public Animator eyebrow;
    public Animator eyes;
    public Animator mouth;

    private void OnEnable()
    {
        inputHandler.onAbilityTriggered += OnAbilityTriggered;
    }

    private void OnDisable()
    {
        inputHandler.onAbilityTriggered -= OnAbilityTriggered;
    }

    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
    }

    void Update()
    {
        eyebrow.SetFloat("Eyebrow Value", inputHandler.eyeBrowsValue);
        eyes.SetFloat("Eye Value", inputHandler.eyesValue);
        mouth.SetFloat("Mouth Value", inputHandler.mouthValue);
    }

    private void OnAbilityTriggered(Ability ability)
    {
        if (ability.eyebrowsChangeValue > 0) eyebrow.SetTrigger("Eyebrow Feedback");
        if (ability.eyebrowsChangeValue < 0) eyebrow.transform.DOMoveX(0.2f, 0.5f).SetEase(Ease.Flash,10,0.5f);

        if (ability.eyesChangeValue > 0) eyes.SetTrigger("Eye Feedback");
        if (ability.eyesChangeValue < 0) eyes.transform.DOMoveX(0.2f, 0.5f).SetEase(Ease.Flash,10,0.5f);

        if (ability.mouthChangeValue > 0) mouth.SetTrigger("Mouth Feedback");
        if (ability.mouthChangeValue < 0) mouth.transform.DOMoveX(0.2f, 0.5f).SetEase(Ease.Flash,10,0.5f);
        
    }
}
