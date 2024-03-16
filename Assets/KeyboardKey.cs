using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class KeyboardKey : MonoBehaviour
{
    [Header("Key Settings")]
    public KeyCode TargetKey;
    public float Cooldown;
    bool usingCooldown = false;
    bool isCooldown = false;

    [Header("Event Settings")]
    public UnityEvent OnKeyDown;
    public UnityEvent OnKeyPress;
    public UnityEvent OnKeyUp;
    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        if (Cooldown > 0)
        {
            usingCooldown = true;
        }
        // inputField.onEndEdit.AddListener(delegate { ClearInputField(); });
    }

    // Update is called once per frame
    void Update()
    {
        InvokeKey();
    }

    void StartCooldown()
    {
        isCooldown = false;
    }

    void InvokeKey()
    {
        if (Input.GetKeyDown(TargetKey))
        {
            Debug.Log("Key Down!");
            if (usingCooldown)
            {
                if (!isCooldown)
                {
                    OnKeyDown.Invoke();
                    isCooldown = true;
                    Invoke("StartCooldown", Cooldown);
                }
            }
            else
            {
                OnKeyDown.Invoke();
            }
        }
        if (Input.GetKey(TargetKey))
        {
            Debug.Log("Key Pressed!");
            if (usingCooldown)
            {
                if (!isCooldown)
                {
                    OnKeyPress.Invoke();
                    isCooldown = true;
                    Invoke("StartCooldown", Cooldown);
                }
            }
            else
            {
                OnKeyPress.Invoke();
            }
        }
        if (Input.GetKeyUp(TargetKey))
        {
            Debug.Log("Key Up!");
            if (usingCooldown)
            {
                if (!isCooldown)
                {
                    OnKeyUp.Invoke();
                    isCooldown = true;
                    Invoke("StartCooldown", Cooldown);
                }
            }
            else
            {
                OnKeyUp.Invoke();
            }
        }
    }

    public void ClearInputField()
    {
        inputField.text = "";
    }
}
