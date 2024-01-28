using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    public Image cooldownMask;

    private float cooldownTimer;

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
