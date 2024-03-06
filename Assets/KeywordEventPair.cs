using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class KeywordUnityEventPair : MonoBehaviour
{
    public string keyword;
    public UnityEvent unityEvent;
}

public class InputFieldKeywordUnityEventTrigger : MonoBehaviour
{
    public TMP_InputField inputField; // Assign in the inspector
    public List<KeywordUnityEventPair> keywordEvents; // Define keywords and their events in the inspector

    void Start()
    {
        inputField.onEndEdit.AddListener(CheckKeywordAndTriggerEvent);
    }

    private void CheckKeywordAndTriggerEvent(string input)
    {
        foreach (var pair in keywordEvents)
        {
            if (input.Equals(pair.keyword, System.StringComparison.OrdinalIgnoreCase))
            {
                pair.unityEvent.Invoke();
                return; // Exit after the first match
            }
        }
        // Optional: Add logic here if no keyword matches
    }
}
