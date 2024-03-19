using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public string CompareText;
    public UnityEvent TrueEvents;
    public UnityEvent FalseEvents;

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
            if (ifSame)
            {
                if (CurrentVariable.CurrentValue == CompareText)
                {
                    TrueEvents?.Invoke();
                }
                if (CurrentVariable.CurrentValue != CompareText)
                {
                    FalseEvents?.Invoke();
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvents?.Invoke();
        ConditionChecking();
    }
}
