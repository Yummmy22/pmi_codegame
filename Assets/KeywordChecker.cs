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
    public AudioHandler audioHandler;
    public CalculationString m_string;
    public List<KeywordEvents> KeywordEvents;
    public List<KeywordEvent> keywordEventsAdv;
    public UnityEvent FalseEvent;
    bool ConditionHasMatched;
    bool PowerUpActivated = false;
    public void ActivePowerUp(){
        PowerUpActivated = true;
    }
    
    public void CheckKeywords()
    {
        foreach (var keywordEvent in KeywordEvents)
        {
            if (keywordEvent.Keyword == m_string.CurrentValue)
            {
                keywordEvent.OnKeywordMatch.Invoke();
                ConditionHasMatched = true;
                audioHandler.PlayCorrectSound();
                InputField.text = "";
                break;
            }
        }

        if (PowerUpActivated == true)
        {
            foreach (var keywordEventAdv in keywordEventsAdv)
            {
                if (keywordEventAdv.Keyword == m_string.CurrentValue)
                {
                    keywordEventAdv.OnKeywordMatch.Invoke();
                    ConditionHasMatched = true;
                    audioHandler.PlayCorrectSound();
                    InputField.text = "";
                    break;
                }
            }
        }
        if (!ConditionHasMatched)
        {
            CameraShaker.Invoke();
            audioHandler.PlayWrongSound();
            FalseEvent.Invoke();
        }
        ConditionHasMatched = false; //reset condition
    }
}
