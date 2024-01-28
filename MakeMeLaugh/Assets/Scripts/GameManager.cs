using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) ProceedToNextLevel();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float alpha = canvasGroup.alpha;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = alpha;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float alpha = canvasGroup.alpha;

        while (alpha > 0f)
        {
            // print(alpha);
            alpha -= Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = alpha;
            yield return null;
        }
    }

    public void ProceedToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartCoroutine(LoadScene(0));
            // SceneManager.LoadScene(0);
        }
    }

    private IEnumerator LoadScene(int buildIndex)
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(buildIndex);
    }
}
