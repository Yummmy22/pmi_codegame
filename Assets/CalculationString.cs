using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CalculationString : MonoBehaviour
{

    [Header("Current Value")]
    public string CurrentValue;

    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;

    // Start is called before the first frame update
    void Start()
    {
        StartEvents?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvents?.Invoke();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(transform.gameObject.name, CurrentValue);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(transform.name))
        {
            CurrentValue = PlayerPrefs.GetString(transform.name);
        }
    }

    public void SetCurrentValue(string aValue)
    {
        CurrentValue = aValue;
    }

    public void SetCurrentValue(Text aValue)
    {
        CurrentValue = aValue.text;
    }

    public void WriteTo(InputField aValue)
    {
        aValue.text = CurrentValue;
    }

    public void WriteTo(Text aValue)
    {
        aValue.text = CurrentValue.ToString();
    }

    public void WriteTo(TextMesh aValue)
    {
        aValue.text = CurrentValue.ToString();
    }

    public void ReadFrom(InputField aValue)
    {
        CurrentValue = aValue.text;
    }
    public void ReadFrom(TMP_InputField aValue)
    {
        CurrentValue = aValue.text;
    }
}
