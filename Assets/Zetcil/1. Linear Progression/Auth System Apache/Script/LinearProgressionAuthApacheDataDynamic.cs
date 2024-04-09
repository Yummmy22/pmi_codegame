using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;

public class LinearProgressionAuthApacheDataDynamic : MonoBehaviour
{

    [Header("Auth Server")]
    [TextArea(3, 4)]
    public string Apache = "http://localhost/";
    public string Segment = "save_progress/";

    [Header("Primary Data")]
    public string TableName;
    public string Username;
    public string Activity;
    public string Scene;
    public Text Score;

    [Header("Secondary Data")]
    public bool SendSecondaryData;
    [TextArea(4, 5)]
    public string Problem;
    [TextArea(4, 5)]
    public string Solution;
    [TextArea(4, 5)]
    public string Notes;
    public string Organization;
    public Text Answer;
    public Text Duration;


    [Header("Field Settings")]
    public string FieldTablename = "tablename";
    public string FieldActivity = "activity";
    public string FieldEmail = "email";
    public string FieldUsername = "username";
    public string FieldPassword = "password";
    public string FieldScene = "scene";
    public string FieldFirstName = "first_name";
    public string FieldLastName = "last_name";
    public string FieldGender = "gender";
    public string FieldOrganization = "organization";
    public string FieldProblem = "problem";
    public string FieldSolution = "solution";
    public string FieldAnswer = "answer";
    public string FieldDuration = "duration";
    public string FieldScore = "score";
    public string FieldNotes = "notes";

    [Header("Events")]
    public UnityEvent SaveProgressSuccessEvent;
    public UnityEvent SaveProgressFailureEvent;
    public UnityEvent UnknownEvent;

    [Header("System")]
    public string CurrentEmail;
    public string CurrentPass;

    string RequestStatus = "";
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InitializeServer", 1);
    }

    void Initialize()
    {
        Apache = "Connection Error";
        Activity = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        Organization = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_SCHOOL);
        Username = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_USER);
        Apache = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_SERVER, Apache);
        CurrentEmail = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_EMAIL, "EMAIL");
        CurrentPass = PlayerPrefs.GetString(LinearProgressionAuthApache.CURRENT_PASS, "PASSWORD");

    }
    public void InvokeProgress()
    {
        StartCoroutine(ExecuteProgress());
    }

    IEnumerator ExecuteProgress()
    {
        WWWForm form = new WWWForm();
        form.AddField(FieldTablename, TableName);
        form.AddField(FieldActivity, Activity);
        form.AddField(FieldOrganization, Organization);
        form.AddField(FieldUsername, Username);
        form.AddField(FieldScene, Scene);
        form.AddField(FieldProblem, Problem);
        form.AddField(FieldSolution, Solution);
        form.AddField(FieldAnswer, Answer.text);
        form.AddField(FieldDuration, Duration.text);
        form.AddField(FieldScore, Score.text);
        form.AddField(FieldNotes, Notes);

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
                SaveProgressSuccessEvent.Invoke();
            }
            else if (RequestStatus == LinearProgressionAuthApache.USERS_EXISTS)
            {
                SaveProgressFailureEvent.Invoke();
            }
            else
            {
                UnknownEvent.Invoke();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
