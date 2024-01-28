using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Image image;
    public Sprite laugh;
    public GameObject text;

    private bool hasStarted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hasStarted)
            {
                StartCoroutine(StartTheGame());
                hasStarted = true;
            }
        }
    }

    private IEnumerator StartTheGame()
    {
        image.sprite = laugh;
        text.SetActive(false);
        yield return new WaitForSeconds(.5f);
        GameManager.Instance.ProceedToNextLevel();
    }
}
