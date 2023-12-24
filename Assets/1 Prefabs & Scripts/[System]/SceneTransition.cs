using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public float transitionTime = 1.0f; // Время затухания и растухания
    public Image transitionImage; // Изображение для затенения

    private void Start()
    {
        // Начать корутину с затуханием
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        // Запустить корутину с растуханием перед загрузкой новой сцены
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime / transitionTime;
            transitionImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / transitionTime;
            transitionImage.color = new Color(0, 0, 0, t);
            yield return null;
        }

        // Загрузить новую сцену
        SceneManager.LoadScene(sceneName);
    }
}
