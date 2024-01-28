using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    public Image cooldownMask;

    public float yPos = 150;

    private float cooldownTimer;

    private void OnEnable()
    {
        transform.DOLocalMoveY(-200, 0);
        transform.DOLocalMoveY(yPos, 1f).SetEase(Ease.OutElastic,2,1);
    }

    private void OnDisable()
    {
        transform.DOLocalMoveY(-200, 1f).SetEase(Ease.OutElastic,2,1);
    }

    private void Start()
    {
        cooldownMask.fillAmount = 0;
    }

    public IEnumerator CooldownEffect(float cooldownDuration)
    {
        float elapsed = 0f;
        while (elapsed < cooldownDuration)
        {
            elapsed += Time.deltaTime;
            cooldownMask.fillAmount = Mathf.Lerp(1.0f, 0f, elapsed / cooldownDuration);
            yield return null;
        }
        cooldownMask.fillAmount = 0f;
    }
}
