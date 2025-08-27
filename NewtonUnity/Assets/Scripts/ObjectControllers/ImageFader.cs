using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public Image image;          // Assign your UI Image here
    public float fadeDuration = 1f;  // Time for fade in/out
    public float holdTime = 0.5f;    // Time to stay fully visible/invisible

    private void Start()
    {
        if (image != null)
            StartCoroutine(FadeLoop());
    }

    private System.Collections.IEnumerator FadeLoop()
    {
        while (true)
        {
            // Fade In
            yield return StartCoroutine(Fade(0f, 1f));
            yield return new WaitForSeconds(holdTime);

            // Fade Out
            yield return StartCoroutine(Fade(1f, 0f));
            yield return new WaitForSeconds(holdTime);
        }
    }

    private System.Collections.IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = image.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            image.color = c;
            yield return null;
        }
    }
}
