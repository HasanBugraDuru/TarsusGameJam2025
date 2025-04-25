
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeManager : MonoBehaviour
{
    public static SceneFadeManager instance;
    [SerializeField] private Image fadePanel;
    [Range(0.1f, 10f), SerializeField] private float fadeOutSpeed = 5f;
    [Range(0.1f, 10f), SerializeField] private float fadeInSpeed = 5f;
    [SerializeField] private Color fadeStartColor = Color.black;

    public bool IsFadingOut { get; private set; }
    public bool IsFadingIn { get; private set; }

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
            return;
        }

        // Baþlangýçta transparan yap
        fadeStartColor.a = 0f;
        fadePanel.color = fadeStartColor;
    }

    private void Update()
    {
        if (IsFadingOut)
        {
            if (fadePanel.color.a < 1f)
            {
                Color currentColor = fadePanel.color;
                currentColor.a += Time.deltaTime * fadeOutSpeed;
                fadePanel.color = currentColor;
            }
            else
            {
                IsFadingOut = false;
            }
        }

        if (IsFadingIn)
        {
            if (fadePanel.color.a > 0f)
            {
                Color currentColor = fadePanel.color;
                currentColor.a -= Time.deltaTime * fadeInSpeed;
                fadePanel.color = currentColor;
            }
            else
            {
                IsFadingIn = false;
                // Fade tamamen bitince paneli kapat
                fadePanel.gameObject.SetActive(false);
            }
        }
    }

    public void StartFadeOut()
    {
        fadePanel.gameObject.SetActive(true);
        IsFadingOut = true;
    }

    public void StartFadeIn()
    {
        IsFadingIn = true;
    }
}