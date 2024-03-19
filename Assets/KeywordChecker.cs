using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class KeywordEvents
{
    public string Keyword;
    public UnityEvent OnKeywordMatch;
}

public class KeywordChecker : MonoBehaviour
{
    public TMP_InputField InputField;
    public CalculationString m_string;
    public List<KeywordEvents> KeywordEvents;
    public UnityEvent FalseEvent;
    bool ConditionHasMatched;
    // Start is called before the first frame update
    public void CheckKeywords()
    {
        print(m_string.CurrentValue);
        foreach (var keywordEvent in KeywordEvents)
        {
            if (keywordEvent.Keyword == m_string.CurrentValue)
            {
                keywordEvent.OnKeywordMatch.Invoke();
                ConditionHasMatched = true;
                InputField.text = "";
                break;
            }

        }
        if (!ConditionHasMatched)
        {
            FalseEvent.Invoke();
        }
        ConditionHasMatched = false; //reset condition
    }
}
