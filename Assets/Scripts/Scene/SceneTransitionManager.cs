using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Sahne ge�i�i metodu (butonlar ve ge�i� alanlar� i�in)
    public static void ChangeScene(string sceneName)
    {
        instance.StartCoroutine(instance.FadeOutThenChangeScene(sceneName));
    }

    private IEnumerator FadeOutThenChangeScene(string sceneName)
    {
        // Fade out ba�lat
        SceneFadeManager.instance.StartFadeOut();

        // Fade out tamamlanana kadar bekle
        while (SceneFadeManager.instance.IsFadingOut)
        {
            yield return null;
        }

        // Sahneyi y�kle
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Yeni sahne y�klendi�inde fade in ba�lat
        SceneFadeManager.instance.StartFadeIn();
    }
}
