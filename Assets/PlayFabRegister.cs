using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabRegister : MonoBehaviour
{
    [Header("PlayFab Settings")]
    public string PlayFabID;

    [Header("Register Settings")]
    public InputField emailInput;
    public InputField usernameInput;
    public InputField displaynameInput;
    public InputField passwordInput;
    public InputField confirmpassInput;
    public bool UpdateDisplayName;

    [Header("Event Settings")]
    public UnityEvent StartEvents;
    public UnityEvent UpdateEvents;

    [Header("Success Settings")]
    public UnityEvent OnRegisterSuccessEvents;
    public UnityEvent OnUpdateDisplayNameSuccessEvents;

    [Header("Error Settings")]
    public UnityEvent OnRegisterErrorEvents;
    public UnityEvent OnUpdateDisplayNameErrorEvents;
    public UnityEvent OnUsernameLengthEvents;
    public UnityEvent OnPasswordLengthEvents;
    public UnityEvent OnPasswordConfirmationEvents;

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

    // Update is called once per frame
    public void InvokeRegister()
    {
        if (usernameInput.text.Length < 3)
        {
            Debug.Log("PlayFab Register: User Length Error");
            OnUsernameLengthEvents?.Invoke();
        }
        if (passwordInput.text.Length < 6)
        {
            Debug.Log("PlayFab Register: Password Length Error");
            OnPasswordLengthEvents?.Invoke();
        }
        if (passwordInput.text != confirmpassInput.text)
        {
            Debug.Log("PlayFab Register: Password Confirmation Error");
            OnPasswordConfirmationEvents?.Invoke();
        }
        else
        {

            var request = new RegisterPlayFabUserRequest
            {
                Email = emailInput.text,
                Username = usernameInput.text,
                Password = passwordInput.text,
                RequireBothUsernameAndEmail = true
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
        }
    }
    public void InvokeUpdateDisplayName()
    {
        string newDisplayName = displaynameInput.text;

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newDisplayName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnDisplayNameUpdateError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("PlayFab Register: Success");
        OnRegisterSuccessEvents?.Invoke();
        if (UpdateDisplayName)
        {
            InvokeUpdateDisplayName();
        }
    }

    void OnRegisterError(PlayFabError result)
    {
        Debug.Log("PlayFab Register: Error (" + result.GenerateErrorReport() + ")");
        OnRegisterErrorEvents?.Invoke();
    }

    private void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("DisplayName updated successfully: " + result.DisplayName);
        OnUpdateDisplayNameSuccessEvents?.Invoke();
        // Refresh leaderboard or update UI as needed.
    }

    private void OnDisplayNameUpdateError(PlayFabError error)
    {
        Debug.LogError("Error updating DisplayName: " + error.ErrorMessage);
        OnUpdateDisplayNameErrorEvents?.Invoke();
        // Handle error, if needed.
    }
}

