using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;

    public string startingDialogue = "Ugh... My- my head hurts so bad...";
    public float fadeDuration = 0.5f; // fade in/out duration

    void Start()
    {
        StartCoroutine(ShowDialogue(true, startingDialogue, 5f));
    }

    public IEnumerator ShowDialogue(bool isThought, string text, float displayTime)
    {
        if (isThought)
        {
            text = "<i>" + text + "</i>";
        }

        dialogueText.text = text;
        Color c = dialogueText.color;

        // Fade in
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Clamp01(timer / fadeDuration);
            dialogueText.color = c;
            dialogueText.enabled = true;
            yield return null;
        }

        c.a = 1f;
        dialogueText.color = c;
        yield return new WaitForSeconds(displayTime);

        // Fade out
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Clamp01(1f - (timer / fadeDuration));
            dialogueText.color = c;
            yield return null;
        }

        dialogueText.enabled = false;
    }
}
