using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.Events;

public class TriggerConversation : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private bool conversationStarted = false;
    public UnityEvent onConversationStart;
    public UnityEvent onConversationEnd;


    // Start is called before the first frame update
    void Update()
    {
        if (Input.anyKey && !conversationStarted)
        {
            conversationStarted = true;
            onConversationStart.Invoke();
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.OnConversationEnded -= OnConversationEnded;
            ConversationManager.OnConversationEnded += OnConversationEnded;
        }
    }

    private void OnConversationEnded()
    {
        onConversationEnd.Invoke();
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
}
