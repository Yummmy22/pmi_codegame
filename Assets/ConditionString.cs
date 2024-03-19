using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class KeywordEvent
{
    public string Keyword;
    public UnityEvent OnKeywordMatch;
}

[System.Serializable]
public class KeywordEventPair
{
    public List<KeywordEvent> KeywordEvents;
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
    // public bool ifSame;
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
            bool conditionMatched = false; // Flag to indicate if a condition has been matched

            // Iterate through all KeywordEventPairs
            foreach (var keywordEventPair in KeywordEvents)
            {
                // Iterate through all KeywordEvents within the KeywordEventPair
                foreach (var keywordEvent in keywordEventPair.KeywordEvents)
                {
                    // Check if the CurrentVariable.CurrentValue matches the specified keyword
                    if (CurrentVariable.CurrentValue == keywordEvent.Keyword)
                    {
                        keywordEvent.OnKeywordMatch?.Invoke(); // Use the new member name here
                        conditionMatched = true;
                        break; // Exit the inner loop if a keyword matches
                    }
                }

                if (conditionMatched)
                {
                    break; // Exit the outer loop if a condition has been matched
                }
            }

            // If no keywords matched, trigger the global FalseEvent
            if (!conditionMatched)
            {
                foreach (var keywordEventPair in KeywordEvents)
                {
                    keywordEventPair.FalseEvent?.Invoke();
                    break; // Only need to invoke one global FalseEvent
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