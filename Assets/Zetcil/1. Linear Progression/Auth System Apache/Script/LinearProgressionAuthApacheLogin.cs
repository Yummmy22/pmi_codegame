using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
public class LinearProgressionAuthApacheLogin : MonoBehaviour
{
    [Header("Auth Server")]
    [TextArea(3, 4)]
    public string Apache = "http://localhost/";
    public string Segment = "login/";

    [Header("Auth Settings")]
    public InputField Email;
    public InputField Password;

    [Header("Events")]
    public UnityEvent LoginSuccessEvent;
    public UnityEvent LoginFailureEvent;
    public UnityEvent UnknownEvent;
    string RequestStatus = "";
    bool already_login = false;
    bool email_focused;
    bool pass_focused;


    // Start is called before the first frame update
    void Start()
    {
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
        if (!already_login && Input.GetKeyDown(KeyCode.Return))
        {
            if (email_focused)
            {
                Password.Select();

            }
            else if (pass_focused)
            {
                InvokeAuthLogin();
            }
        }
        if (already_login && Input.GetKeyDown(KeyCode.Return))
        {
            this.gameObject.SetActive(false);
        }
        email_focused = Email.isFocused;
        pass_focused = Password.isFocused;
    }

    public void SetCurrentSession() 
    {
        PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_EMAIL, Email.text);
        PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_PASS, Password.text);
    }

    public void InvokeAuthLogin()
    {
        SetCurrentSession();
        StartCoroutine(ExecuteLogin());
    }

    IEnumerator ExecuteLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", Email.text);
        form.AddField("password", Password.text);

        using (UnityWebRequest www = UnityWebRequest.Post(Apache + Segment, form))
        {
            yield return www.SendWebRequest();

            //-- 2019
            if (www.result != UnityWebRequest.Result.Success)
            {
                RequestStatus = www.error;
            }
            else
            {
                RequestStatus = www.downloadHandler.text;
            }

            Debug.Log(RequestStatus);
            PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_URL, Apache + Segment);
            PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_STATUS, RequestStatus);

            if (RequestStatus == LinearProgressionAuthApache.LOGIN_SUCCESS)
            {
                StartCoroutine(ExecuteLoginData());
                LoginSuccessEvent.Invoke();
                already_login = true;
            }
            else if (RequestStatus == LinearProgressionAuthApache.LOGIN_FAILED)
            {
                LoginFailureEvent.Invoke();
            }
            else
            {
                UnknownEvent.Invoke();
            }
        }
    }

    IEnumerator ExecuteLoginData()
    {
        yield return null;
    }
}
