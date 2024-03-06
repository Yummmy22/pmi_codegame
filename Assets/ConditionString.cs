using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] // This makes the class show up in the Unity inspector
public class KeywordEventPair
{
    public string Keyword;
    public UnityEvent TrueEvent;
    public UnityEvent FalseEvent;
}

public class ConditionString : MonoBehaviour
{
    [Header("Variable Settings")]
    public CalculationString CurrentVariable;
    public bool Activated;

    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;

    [Header("Condition Settings")]
    public bool ifSame;
    public List<KeywordEventPair> KeywordEvents; // Use a list of KeywordEventPair

    public void StartChecking()
    {
        Activated = true;
    }

    public void StopChecking()
    {
        Activated = false;
    }

    void ConditionChecking()
    {
        if (Activated)
        {
            foreach (var keywordEvent in KeywordEvents) // Iterate through all keyword-event pairs
            {
                if (ifSame && CurrentVariable.CurrentValue == keywordEvent.Keyword)
                {
                    keywordEvent.TrueEvent?.Invoke(); // Invoke the True event for this keyword
                    return; // Exit the function after finding a match to prevent multiple events
                }
                else
                {
                    keywordEvent.FalseEvent?.Invoke(); // Invoke the False event if not matching
                    // Note: If you want only one False event to trigger if none match, move this outside and trigger after the loop if no matches found
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartEvents?.Invoke();
        if (Activated)
        {
            StartChecking();
            ConditionChecking();
            StopChecking();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvents?.Invoke();
        ConditionChecking();
    }
}
