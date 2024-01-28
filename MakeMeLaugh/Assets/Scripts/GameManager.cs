using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1.5f;

    [HideInInspector] public bool isInGame;

    private RawImage transitionMask;
    private Image outroMask;
    private TextMeshProUGUI line;
    private Face[] faceArray;
    private GameObject iconHolder;

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
        transitionMask = canvasGroup.GetComponentInChildren<RawImage>();
        outroMask = canvasGroup.GetComponentInChildren<Image>();
        line = canvasGroup.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    void Start()
    {
        // emoticon = GameObject.Find("Emoticon");
        // iconHolder = GameObject.Find("Icon Holder");
        // emoticon.SetActive(false);
        // iconHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PressF5ResetGame();
        if(Input.GetKeyDown(KeyCode.A)) PlayLevelOutro();
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
        // StartCoroutine(FadeOut());
        if (scene.buildIndex == 0) return;

        faceArray = FindObjectsOfType<Face>();
        iconHolder = GameObject.Find("Icon Holder");
        
        isInGame = false;
        foreach (var face in faceArray) face.gameObject.SetActive(false);
        iconHolder.SetActive(false);

        outroMask.rectTransform.DOLocalMoveX(2000, 0);
        transitionMask.rectTransform.DOLocalMoveX(0, 0);
        transitionMask.rectTransform.DOLocalMoveX(-2000, 0.5f).SetEase(Ease.InQuad).OnComplete((() =>
        {
            Sequence openingSequence = DOTween.Sequence();
            switch (scene.buildIndex)
            {
                case 1:
                    openingSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("Making someone laugh is hard", 0f))
                        .AppendInterval(2)
                        .Append(line.DOText("", 0))
                        .AppendInterval(0.5f)
                        .OnComplete((() =>
                        {
                            isInGame = true;
                            foreach (var face in faceArray) face.gameObject.SetActive(true);
                            iconHolder.SetActive(true);
                        }));
                    openingSequence.Play();
                    break;
                case 2:
                    openingSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("You can always use multiple techniques", 0f))
                        .AppendInterval(2)
                        .Append(line.DOText("", 0))
                        .AppendInterval(0.5f)
                        .OnComplete((() =>
                        {
                            isInGame = true;
                            foreach (var face in faceArray) face.gameObject.SetActive(true);
                            iconHolder.SetActive(true);
                        }));
                    openingSequence.Play();
                    break;
                case 3:
                    openingSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("But even one person may react differently to various kinds of humor.", 0f))
                        .AppendInterval(2)
                        .Append(line.DOText("", 0))
                        .AppendInterval(0.5f)
                        .OnComplete((() =>
                        {
                            isInGame = true;
                            foreach (var face in faceArray) face.gameObject.SetActive(true);
                            iconHolder.SetActive(true);
                        }));
                    openingSequence.Play();
                    break;
                case 4:
                    openingSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("Reaction are often mixed and ambiguous", 0f))
                        .AppendInterval(2)
                        .Append(line.DOText("", 0))
                        .AppendInterval(0.5f)
                        .OnComplete((() =>
                        {
                            isInGame = true;
                            foreach (var face in faceArray) face.gameObject.SetActive(true);
                            iconHolder.SetActive(true);
                        }));
                    openingSequence.Play();
                    break;
                case 5:
                    openingSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("So you'll need to keep tweaking your strategy.", 0f))
                        .AppendInterval(2)
                        .Append(line.DOText("", 0))
                        .AppendInterval(0.5f)
                        .OnComplete((() =>
                        {
                            isInGame = true;
                            foreach (var face in faceArray) face.gameObject.SetActive(true);
                            iconHolder.SetActive(true);
                        }));
                    openingSequence.Play();
                    break;
                case 6:
                    openingSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("Everyone’s got their own comedy groove.", 0f))
                        .AppendInterval(2)
                        .Append(line.DOText("", 0))
                        .AppendInterval(0.5f)
                        .OnComplete((() =>
                        {
                            isInGame = true;
                            foreach (var face in faceArray) face.gameObject.SetActive(true);
                            iconHolder.SetActive(true);
                        }));
                    openingSequence.Play();
                    break;
                case 7:
                    openingSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("As the quest reaches a zenith of fervor and the trials intensify...", 0f))
                        .AppendInterval(2)
                        .Append(line.DOText("", 0))
                        .AppendInterval(0.5f)
                        .OnComplete((() =>
                        {
                            isInGame = true;
                            foreach (var face in faceArray) face.gameObject.SetActive(true);
                            iconHolder.SetActive(true);
                            Invoke("PlayLevelOutro", 10f);
                        }));
                    openingSequence.Play();
                    break;
                default:
                    break;
            }
        }));
    }

    public void OnLevelPassed()
    {
        isInGame = false;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            foreach (var icon in iconHolder.GetComponentsInChildren<AbilityIcon>()) icon.enabled = false;
        }
    }

    public void PlayLevelOutro()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            foreach (var icon in iconHolder.GetComponentsInChildren<AbilityIcon>()) icon.enabled = false;
            Sequence outroSequence = DOTween.Sequence();
            outroSequence
                .Append(Camera.main.DOOrthoSize(0.75f, 6f))
                .AppendInterval(3)
                .Append(outroMask.rectTransform.DOLocalMoveX(2000, 0))
                .Append(outroMask.rectTransform.DOLocalMoveX(0, 1.5f).SetEase(Ease.InQuad))
                .Append(line.DOText("", 0f))
                .AppendInterval(1)
                .Append(line.DOText("you might find yourself pondering the very essence of your endeavor.", 0f))
                .AppendInterval(3)
                .Append(line.DOText("How to Make Laugh",0f))
                .AppendInterval(2)
                .Append(line.DOText("Made for GGJ 2024\nby Blaer, Linke & Haotian",0f))
                .AppendInterval(3)
                .Append(line.DOText("", 0f))
                .OnComplete((() =>
                {
                    ProceedToNextLevel();
                }));
            outroSequence.Play();
        }
        
        if (SceneManager.GetActiveScene().buildIndex <= 6)
        {
            outroMask.rectTransform.DOLocalMoveX(2000, 0);
        outroMask.rectTransform.DOLocalMoveX(0, 1.5f).SetEase(Ease.InQuad).OnComplete((() =>
        {
            Sequence outroSequence = DOTween.Sequence();
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    outroSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("...and sometimes, it's surprisingly simple.", 0f))
                        .AppendInterval(2)
                        .OnComplete((() =>
                        {
                            ProceedToNextLevel();
                        }));
                    outroSequence.Play();
                    break;
                case 2:
                    outroSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("Blending and variation usually does the trick.", 0f))
                        .AppendInterval(2)
                        .OnComplete((() =>
                        {
                            ProceedToNextLevel();
                        }));
                    outroSequence.Play();
                    break;
                case 3:
                    outroSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("Some techniques may well backfire", 0f))
                        .AppendInterval(2)
                        .OnComplete((() =>
                        {
                            ProceedToNextLevel();
                        }));
                    outroSequence.Play();
                    break;
                case 4:
                    outroSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("This complexity adds a layer of challenge to the endeavor.", 0f))
                        .AppendInterval(2)
                        .OnComplete((() =>
                        {
                            ProceedToNextLevel();
                        }));
                    outroSequence.Play();
                    break;
                case 5:
                    outroSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("and be adaptable.", 0f))
                        .AppendInterval(2)
                        .OnComplete((() =>
                        {
                            ProceedToNextLevel();
                        }));
                    outroSequence.Play();
                    break;
                case 6:
                    outroSequence
                        .Append(line.DOText("", 0f))
                        .AppendInterval(1)
                        .Append(line.DOText("which makes it even harder to delight two at once.", 0f))
                        .AppendInterval(2)
                        .OnComplete((() =>
                        {
                            ProceedToNextLevel();
                        }));
                    outroSequence.Play();
                    break;
                default:
                    break;
            }
        }));
        }
    }
     
    public void ProceedToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
            transitionMask.rectTransform.DOLocalMoveX(2000, 0);
            transitionMask.rectTransform.DOLocalMoveX(0, 0.5f).SetEase(Ease.InQuad).OnComplete((() =>
            {
                line.DOText("", 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }));
        }
        else
        {
            // StartCoroutine(LoadScene(0));
            transitionMask.rectTransform.DOLocalMoveX(2000, 0);
            transitionMask.rectTransform.DOLocalMoveX(0, 0.5f).SetEase(Ease.InQuad).OnComplete((() =>
            {
                line.DOText("", 0);
                SceneManager.LoadScene(0);
                outroMask.rectTransform.DOLocalMoveX(2000, 0);
                transitionMask.rectTransform.DOLocalMoveX(0, 0);
                transitionMask.rectTransform.DOLocalMoveX(-2000, 0.5f).SetEase(Ease.InQuad);
            }));
        }
    }

    void PressF5ResetGame()
    {
        if (Input.GetKeyUp(KeyCode.F5)) SceneManager.LoadScene(0);
    }

    private IEnumerator LoadScene(int buildIndex)
    {
        // StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(buildIndex);
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

}
