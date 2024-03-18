using System;
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
    public List<KeywordEventPair> KeywordEvents;

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
            bool isAnyTrueEventInvoked = false;
            foreach (var keywordEvent in KeywordEvents)
            {
                if (ifSame && CurrentVariable.CurrentValue == keywordEvent.Keyword)
                {
                    keywordEvent.TrueEvent?.Invoke();
                    isAnyTrueEventInvoked = true;
                    return; // Exit the function after finding a match to prevent multiple events
                }
            }

            if (!isAnyTrueEventInvoked)
            {
                // Sekarang langsung memicu FalseEvent tanpa delay
                foreach (var keywordEvent in KeywordEvents)
                {
                    keywordEvent.FalseEvent?.Invoke();
                    break; // Break after invoking the first FalseEvent to prevent multiple invocations
                }
            }
        }
    }

    void Start()
    {
        StartEvents?.Invoke();
        if (Activated)
        {
            StartChecking();
        }
    }

    void Update()
    {
        UpdateEvents?.Invoke();
        ConditionChecking();
    }
}
