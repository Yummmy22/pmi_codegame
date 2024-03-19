using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;
using System.Runtime.CompilerServices;

public class CalculationBoolean : MonoBehaviour
{
    public ConditionBoolean conditionBooleanScript;
    [Header("Current Value")]
    public bool CurrentValue;
    public bool ForceUpdate;

    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;
    public UnityEvent TrueEvents;
    public UnityEvent FalseEvents;
    bool lastStatus;
    bool somethingChange;

    // Start is called before the first frame update
    void Start()
    {
        StartEvents?.Invoke();
        lastStatus = CurrentValue;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvents?.Invoke();
        if (lastStatus != CurrentValue)
        {
            somethingChange = true;
            lastStatus = CurrentValue;
        }
        if (somethingChange)
        {
            if (CurrentValue) TrueEvents?.Invoke();
            else FalseEvents?.Invoke();
            somethingChange = false;
        }
        if (ForceUpdate)
        {
            if (CurrentValue) TrueEvents?.Invoke();
            else FalseEvents?.Invoke();
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(transform.gameObject.name, CurrentValue ? 1 : 0 );
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(transform.name))
        {
            CurrentValue = PlayerPrefs.GetInt(transform.name) == 1 ? true : false;
            Debug.Log(CurrentValue);
        }
    }

    public void SetCurrentValue(bool aValue)
    {
        CurrentValue = aValue;
        if (CurrentValue == true)
        {
            conditionBooleanScript.Activated = false;
            StartCoroutine(WaitAndTurnOff());
        }
    }

    IEnumerator WaitAndTurnOff()
    {
        yield return new WaitForSeconds(1);
        CurrentValue = false;
        conditionBooleanScript.Activated = false;
    }

    public void WriteTo(Text aValue)
    {
        aValue.text = CurrentValue.ToString();
    }

    public void WriteTo(TextMesh aValue)
    {
        aValue.text = CurrentValue.ToString();
    }

    public void WriteTo(Toggle aValue)
    {
        aValue.isOn = CurrentValue;
    }

    public void ReadFrom(Toggle aValue)
    {
        CurrentValue = aValue.isOn;
    }
}
