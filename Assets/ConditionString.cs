using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] // This makes the class show up in the Unity inspector
public class KeywordEventPair
{
    public List<string> Keywords;
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
            bool conditionMatched = false; // Menandai apakah ada kecocokan kondisi

            // Iterasi melalui semua KeywordEventPair
            foreach (var keywordEvent in KeywordEvents)
            {
                bool allKeywordsMatched = true; // Menandai apakah semua kata kunci cocok

                // Iterasi melalui semua kata kunci dalam KeywordEventPair
                foreach (var keyword in keywordEvent.Keywords)
                {
                    // Cek apakah nilai CurrentValue sama dengan salah satu kata kunci yang ditentukan
                    if (CurrentVariable.CurrentValue != keyword)
                    {
                        allKeywordsMatched = false;
                        break; // Langsung keluar dari loop jika ada satu kata kunci yang tidak cocok
                    }
                }

                // Jika semua kata kunci cocok, jalankan TrueEvent dan tandai bahwa ada kecocokan kondisi
                if (allKeywordsMatched)
                {
                    keywordEvent.TrueEvent?.Invoke();
                    conditionMatched = true;
                    break; // Hentikan iterasi karena kondisi telah terpenuhi
                }
            }

            // Jika tidak ada kecocokan dengan semua keyword yang ditentukan, jalankan FalseEvent
            if (!conditionMatched)
            {
                foreach (var keywordEvent in KeywordEvents)
                {
                    keywordEvent.FalseEvent?.Invoke();
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