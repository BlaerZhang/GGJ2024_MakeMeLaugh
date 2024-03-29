using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public List<Face> faces = new List<Face>();

    public float playInterval;
    private float timeSinceLastPlay;

    public static Action<Ability, Face> onAbilityTriggered;
    public static Action onLevelPassed;

    private bool isLevelPassed = false;

    private void Start()
    {
        timeSinceLastPlay = playInterval;
    }

    void Update()
    {
        if(!GameManager.Instance.isInGame) return;
        if (isLevelPassed) return;
        if (timeSinceLastPlay < playInterval)
        {
            timeSinceLastPlay += Time.deltaTime;
        }
        HandleInput();

        CheckPassValue();
    }

    private void CheckPassValue()
    {
        int passedFace = 0;
        foreach (var face in faces)
        {
            if (face.isPassed) passedFace++;
        }

        if (passedFace == faces.Count)
        {
            isLevelPassed = true;
            onLevelPassed();
            GameManager.Instance.OnLevelPassed();
            GameManager.Instance.Invoke("PlayLevelOutro",3f);
            print("pass the level");
        }
    }

    private void HandleInput()
    {
        Face firstFace = faces[0];
        foreach (var ability in firstFace.abilities)
        {
            int abilityIndex = firstFace.abilities.IndexOf(ability);
            if (Input.GetKeyDown(ability.triggerKey))
            {
                if (timeSinceLastPlay >= playInterval) ability.abilityIcon.cooldownMask.fillAmount = 1;
            }

            if (Input.GetKeyUp(ability.triggerKey))
            {
                if (timeSinceLastPlay >= playInterval)
                {
                    foreach (var a in firstFace.abilities)
                    {
                        StartCoroutine(a.abilityIcon.CooldownEffect(playInterval));
                    }

                    switch (abilityIndex)
                    {
                        case 0:
                            GameManager.Instance.PlayWordSound();
                            break;
                        case 1:
                            GameManager.Instance.PlaySelfSound();
                            break;
                        case 2:
                            GameManager.Instance.PlayDarkSound();
                            break;
                    }

                    foreach (var face in faces)
                    {
                        face.ChangeValue(face.abilities[abilityIndex]);
                        onAbilityTriggered(face.abilities[abilityIndex], face);
                    }

                    timeSinceLastPlay = 0f;
                    
                }
            }
        }
    }
}
