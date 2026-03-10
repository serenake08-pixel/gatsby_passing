using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class OverallScript : MonoBehaviour
{
    Scene currentScene;
    public static bool is2020 = false; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "2020s scene")
        {
            CanvasGroup faderCanvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();
            StartCoroutine(FadeIn(faderCanvasGroup));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.name == "end scene" && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            is2020 = false;
            SceneManager.LoadScene("StartScreen");
        }
    }
    public IEnumerator SwitchTo2020()
    {
        CanvasGroup faderCanvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();
        yield return StartCoroutine(FadeOut(faderCanvasGroup));
        is2020 = true;
        SceneManager.LoadScene("2020s scene");
    }

    public IEnumerator EndGame()
    {
        CanvasGroup faderCanvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();
        yield return StartCoroutine(FadeOut(faderCanvasGroup));
        Debug.Log("Attempting to load end scene...");
        SceneManager.LoadScene("end scene");
    }
    public IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float fadeDuration = 1f; // Duration of the fade-out effect in seconds
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f; // Ensure the canvas is fully opaque at the end
    }
    public IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float fadeDuration = 1f; // Duration of the fade-in effect in seconds
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f; // Ensure the canvas is fully transparent at the end
    }
}
