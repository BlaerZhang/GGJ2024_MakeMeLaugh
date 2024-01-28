using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public float eyeBrowsValue = 0f;
    public float eyesValue = 0f;
    public float mouthValue = 0f;

    public float dampSpeed = 5f;

    public float valueUpLimit = 125f;
    public float passValue = 100f;

    [HideInInspector]
    public bool isPassed = false;

    private bool stopDamping = false;

    public List<Ability> abilities = new List<Ability>();

    private void OnEnable()
    {
        InputHandler.onLevelPassed += StopChange;
    }

    private void OnDisable()
    {
        InputHandler.onLevelPassed -= StopChange;
    }

    void Update()
    {
        if (stopDamping) return;

        DampOutValue();

        CheckPassValue();
    }

    private void StopChange()
    {
        stopDamping = true;
    }

    private void DampOutValue()
    {
        if (eyeBrowsValue > 0) eyeBrowsValue -= dampSpeed * Time.deltaTime;
        else eyeBrowsValue = 0;

        if (eyesValue > 0) eyesValue -= dampSpeed * Time.deltaTime;
        else eyesValue = 0;

        if (mouthValue > 0) mouthValue -= dampSpeed * Time.deltaTime;
        else mouthValue = 0;
    }

    private void CheckPassValue()
    {
        if (eyeBrowsValue >= passValue & eyesValue >= passValue & mouthValue >= passValue)
        {
            // pass level
            print("pass the level");

            isPassed = true;
        }
        else
        {
            isPassed = false;
        }
    }

    public void ChangeValue(Ability ability)
    {
        // eyebrows
        if (ability.eyebrowsChangeValue > 0)
        {
            if (!CheckWhetherOverLimit(eyeBrowsValue, valueUpLimit - ability.eyebrowsChangeValue, 0))
            {
                eyeBrowsValue += ability.eyebrowsChangeValue;
            }
        }
        else if (ability.eyebrowsChangeValue < 0)
        {
            if (!CheckWhetherOverLimit(eyeBrowsValue, valueUpLimit, Mathf.Abs(ability.eyebrowsChangeValue)))
            {
                eyeBrowsValue += ability.eyebrowsChangeValue;
            }
        }

        // eyes
        if (ability.eyesChangeValue > 0)
        {
            if (!CheckWhetherOverLimit(eyesValue, valueUpLimit - ability.eyesChangeValue, 0))
            {
                eyesValue += ability.eyesChangeValue;
            }
        }
        else if (ability.eyesChangeValue < 0)
        {
            if (!CheckWhetherOverLimit(eyesValue, valueUpLimit, Mathf.Abs(ability.eyesChangeValue)))
            {
                eyesValue += ability.eyesChangeValue;
            }
        }

        // mouth
        if (ability.mouthChangeValue > 0)
        {
            if (!CheckWhetherOverLimit(mouthValue, valueUpLimit - ability.mouthChangeValue, 0))
            {
                mouthValue += ability.mouthChangeValue;
            }
        }
        else if (ability.mouthChangeValue < 0)
        {
            if (!CheckWhetherOverLimit(mouthValue, valueUpLimit, Mathf.Abs(ability.mouthChangeValue)))
            {
                mouthValue += ability.mouthChangeValue;
            }
        }
    }

    private bool CheckWhetherOverLimit(float value, float upLimit, float bottomLimit)
    {
        if (value > upLimit || value < bottomLimit) return true;
        return false;
    }
}
