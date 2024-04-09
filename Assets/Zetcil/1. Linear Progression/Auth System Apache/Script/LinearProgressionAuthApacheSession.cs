using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
public class LinearProgressionAuthApacheSession : MonoBehaviour
{
    [Header("Auth Settings")]
    public string CurrentEmail;
    public InputField CurrentEmailField;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InitializeCurrentSession", 1);
    }

    public void InitializeCurrentSession()
    {
        CurrentEmail = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_EMAIL);
        CurrentEmailField.text = CurrentEmail;
    }
}
