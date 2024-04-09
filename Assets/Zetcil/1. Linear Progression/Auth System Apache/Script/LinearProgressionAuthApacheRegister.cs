using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;

public class LinearProgressionAuthApacheRegister : MonoBehaviour
{

    [Header("Auth Server")]
    [TextArea(3, 4)]
    public string Apache = "http://localhost/";
    public string Segment = "register/";

    [Header("Register Settings")]
    public InputField Email;
    public InputField Password;
    public InputField PasswordConfirmation;
    public InputField FirstName;
    public InputField LastName;
    public Toggle GenderM;
    public Toggle GenderF;
    public InputField Organization;

    [Header("Field Settings")]
    public string FieldEmail = "email";
    public string FieldPassword = "password";
    public string FieldFirstName = "first_name";
    public string FieldLastName = "last_name"; 
    public string FieldGender = "gender";
    public string FieldOrganization = "organization";

    [Header("Events")]
    public UnityEvent InvalidEmailEvent;
    public UnityEvent EmptyFirstnameEvent;
    public UnityEvent EmptyPasswordEvent;
    public UnityEvent InvalidConfirmationEvent;
    public UnityEvent RegisterSuccessEvent;
    public UnityEvent RegisterFailureEvent;
    public UnityEvent UnknownEvent;

    string RequestStatus = "";
    bool email_focused;
    bool first_focused;
    bool last_focused;
    bool School_focused;
    bool pass_focused;
    bool passc_focused;

    public void ClearData()
    {
        Email.text = "";
        FirstName.text = "";
        LastName.text = "";
        GenderM.isOn = true;
        Organization.text = "";
        Password.text = "";
        PasswordConfirmation.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearData();
        Invoke("InitializeServer", 1);
    }

    void InitializeServer()
    {
        Apache = "Connection Error";
        if (PlayerPrefs.HasKey(LinearProgressionAuthApache.CURRENT_SERVER))
        {
            Apache = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_SERVER);
        }
        Debug.Log("Server Location: " + Apache);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (School_focused)
            {
                
            }
        }

        email_focused = Email.isFocused;
        first_focused = FirstName.isFocused;
        last_focused = LastName.isFocused;
        School_focused = Organization.isFocused;
        pass_focused = Password.isFocused;
        passc_focused = PasswordConfirmation.isFocused;
    }

    bool isValidate()
    {
        bool result = true;

        if (!Email.text.Contains("@"))
        {
            result = false;
            InvalidEmailEvent.Invoke();
        }
        else if (Password.text == "" || Password.text.Length < 8)
        {
            result = false;
            EmptyPasswordEvent.Invoke();
        }
        else if (Password.text != PasswordConfirmation.text)
        {
            result = false;
            InvalidConfirmationEvent.Invoke();
        }
        else if (FirstName.text == "")
        {
            result = false;
            EmptyFirstnameEvent.Invoke();
        }

        return result;
    }

    public void InvokeRegister()
    {
        if (isValidate())
        {
            StartCoroutine(ExecuteRegister());
        }
    }

    IEnumerator ExecuteRegister()
    {
        WWWForm form = new WWWForm();
        form.AddField(FieldEmail, Email.text);
        form.AddField(FieldPassword, Password.text);
        form.AddField(FieldFirstName, FirstName.text);
        form.AddField(FieldLastName, LastName.text);
        if (GenderM.isOn)
        {
            form.AddField(FieldGender, "M");
        }
        else if (GenderF.isOn)
        {
            form.AddField(FieldGender, "F");
        }
        else
        {
            form.AddField(FieldGender, "X");
        }
        form.AddField(FieldOrganization, Organization.text);

        using (UnityWebRequest www = UnityWebRequest.Post(Apache + Segment, form))
        {
            yield return www.SendWebRequest();


            //-- 2019
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                RequestStatus = www.error;
            }
            else
            {
                RequestStatus = www.downloadHandler.text;
            }

            Debug.Log(RequestStatus);

            if (RequestStatus == LinearProgressionAuthApache.CREATE_USER_SUCCESS)
            {
                RegisterSuccessEvent.Invoke();
                SetCurrentSession();
            }
            else if (RequestStatus == LinearProgressionAuthApache.USERS_EXISTS)
            {
                RegisterFailureEvent.Invoke();
            }
            else
            {
                UnknownEvent.Invoke();
            }
        }
    }

    public void SetCurrentSession()
    {
        PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_EMAIL, Email.text);
        PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_PASS, Password.text);
    }

}
