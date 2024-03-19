using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionBoolean : MonoBehaviour
{
    public CalculationBoolean calculationBooleanScript;
    [Header("Variable Settings")]
    public CalculationBoolean CurrentVariable;
    public bool Activated;

    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;

    [Header("Condition Settings")]
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
        if (calculationBooleanScript.CurrentValue == false)
        {
            if (Activated)
            {
                if (CurrentVariable.CurrentValue == true)
                {
                    TrueEvents?.Invoke();
                }
                if (CurrentVariable.CurrentValue == false)
                {
                    FalseEvents?.Invoke();
                }
            }
        }
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     StartEvents?.Invoke();
    //     if (Activated)
    //     {
    //         StartChecking();
    //     } 
    // }

    // Update is called once per frame
    void Update()
    {
        if (calculationBooleanScript.CurrentValue == false)
        {
            UpdateEvents?.Invoke();
            ConditionChecking();
        }
    }
}




