using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    private float eyeBrowsValue = 0f;
    private float eyesValue = 0f;
    private float mouthValue = 0f;

    public float valueUpLimit = 125f;
    public float passValue = 100f;

    public float dampSpeed = 1f;

    public float playInterval;
    public float timeSinceLastPlay;

    public TMP_Text eyebrow;
    public TMP_Text eye;
    public TMP_Text mouth;

    public List<Ability> abilities = new List<Ability>();

    void Update()
    {
        if (timeSinceLastPlay < playInterval)
        {
            timeSinceLastPlay += Time.deltaTime;
        }
        HandleInput();

        DampOutValue();

        eyebrow.text = ((eyeBrowsValue * 1000)/1000).ToString();
        eye.text = ((eyesValue * 1000)/1000).ToString();
        mouth.text = ((mouthValue * 1000)/1000).ToString();

        CheckPassValue();
    }

    private void DampOutValue()
    {
        if (eyeBrowsValue > 0) eyeBrowsValue -= dampSpeed;
        else eyeBrowsValue = 0;

        if (eyesValue > 0) eyesValue -= dampSpeed;
        else eyesValue = 0;

        if (mouthValue > 0) mouthValue -= dampSpeed;
        else mouthValue = 0;
    }

    private void CheckPassValue()
    {
        if (eyeBrowsValue >= passValue & eyesValue >= passValue & mouthValue >= passValue)
        {
            // pass level
            print("pass the level");
        }
    }

    private void HandleInput()
    {
        foreach (var ability in abilities)
        {
            if (Input.GetKeyDown(ability.triggerKey))
            {
                if (timeSinceLastPlay >= playInterval) ability.abilityIcon.cooldownMask.fillAmount = 1;
            }

            if (Input.GetKeyUp(ability.triggerKey))
            {
                if (timeSinceLastPlay >= playInterval)
                {
                    StartCoroutine(ability.abilityIcon.CooldownEffect(playInterval));

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

                    timeSinceLastPlay = 0f;
                }
            }
        }
    }

    private bool CheckWhetherOverLimit(float value, float upLimit, float bottomLimit)
    {
        if (value > upLimit || value < bottomLimit) return true;
        return false;
    }
}
