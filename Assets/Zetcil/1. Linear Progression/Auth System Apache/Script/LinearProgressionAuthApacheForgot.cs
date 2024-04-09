using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
public class LinearProgressionAuthApacheForgot : MonoBehaviour
{
    [Header("Auth Server")]
    [TextArea(3, 4)]
    public string Apache = "http://localhost/";
    public string Segment = "forgot/";

    [Header("Auth Settings")]
    public InputField Email;

    [Header("Events")]
    public UnityEvent EmailSentEvent;
    public UnityEvent UnknownEvent;
    string RequestStatus = "";

    [Header("System")]
    public string CurrentEmail;
    public string CurrentPass;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InitializeServer", 1);
    }

    void InitializeServer()
    {
        Apache = "Connection Error";
        Apache = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_SERVER, Apache);
        CurrentEmail = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_EMAIL, "EMAIL");
        CurrentPass = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_PASS, "PASSWORD");

        Debug.Log("Server Location: " + Apache);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCurrentSession()
    {
        PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_EMAIL, Email.text);
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

            if (RequestStatus == LinearProgressionAuthApache.CHANGE_PASSWORD_SUCCESS)
            {
                StartCoroutine(ExecuteLoginData());
                EmailSentEvent.Invoke();
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
