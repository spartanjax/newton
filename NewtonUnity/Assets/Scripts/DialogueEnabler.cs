using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEnabler : MonoBehaviour
{
    public DialogueManager DialogueManager;
    private bool happened = false;
    public string who;
    public string text;
    public bool isThought;
    public float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (!happened)
        {
            StartCoroutine(DialogueManager.ShowDialogue(isThought, "<"+who+">: "+text, duration));
        }
        happened = true;

    }
}
