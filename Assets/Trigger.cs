using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    // Define UnityEvents for different stages of the conversation
    public UnityEvent onPlayerEnterTrigger; // Event when the player enters the trigger
    public UnityEvent onPlayerExitTrigger;
    public UnityEvent onPlayerStayTrigger; // Event when the conversation ends

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Invoke the event when the player enters the trigger
            onPlayerEnterTrigger.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onPlayerExitTrigger.Invoke();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            // Invoke the start event and start the conversation
            onPlayerStayTrigger.Invoke();
        }
    }
}
