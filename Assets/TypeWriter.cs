using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class TypeWriter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text textDisplay;

    [Header("Messages (set in Inspector)")]
    [TextArea(2, 5)]
    [SerializeField] private string[] messages;   // Type here in Inspector

    [Header("Timing")]
    [SerializeField] private float typeSpeed = 0.05f;
    [SerializeField] private float pauseBetweenMessages = 0.5f;

    private Queue<string> messageQueue = new Queue<string>();
    private Coroutine typeCoroutine;

    private void Start()
    {
        // Load all Inspector messages into the queue
        if (messages != null && messages.Length > 0)
        {
            foreach (var msg in messages)
            {
                if (!string.IsNullOrEmpty(msg))
                    messageQueue.Enqueue(msg);
            }
            StartNextMessage();
        }
    }


    // Call this to add one or more messages
    public void AddMessages(params string[] messages)
    {
        foreach (string msg in messages)
        {
            messageQueue.Enqueue(msg);
        }
        if (typeCoroutine == null)
        {
            StartNextMessage();
        }
    }

    // Start or continue typing
    private void StartNextMessage()
    {
        if (messageQueue.Count == 0)
        {
            typeCoroutine = null;
            return;
        }

        string currentMessage = messageQueue.Dequeue();
        textDisplay.text = "";
        if (typeCoroutine != null) StopCoroutine(typeCoroutine);
        typeCoroutine = StartCoroutine(TypeAndQueueNext(currentMessage));
    }

    private IEnumerator TypeAndQueueNext(string message)
    {
        textDisplay.maxVisibleCharacters = 0;
        textDisplay.text = message;

        for (int i = 0; i < message.Length; i++)
        {
            textDisplay.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(pauseBetweenMessages);
        StartNextMessage();
    }

    // Optional: Skip current message
    public void Skip()
    {
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
            textDisplay.maxVisibleCharacters = textDisplay.textInfo.characterCount;
            StartNextMessage();
        }
    }
}
