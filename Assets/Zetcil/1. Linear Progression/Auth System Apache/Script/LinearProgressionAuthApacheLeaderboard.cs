using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;

public class LinearProgressionAuthApacheLeaderboard : MonoBehaviour
{

    [Header("Auth Server")]
    [TextArea(3, 4)]
    public string Apache = "http://localhost/";
    public string Segment = "save_leaderboard/";

    [Header("Register Settings")]
    public InputField Username;
    public InputField Score;

    [Header("Field Settings")]
    public string FieldUsername = "name";
    public string FieldScore = "score";

    [Header("Events")]
    public UnityEvent LeaderboardSuccessEvent;
    public UnityEvent LeaderboardFailureEvent;
    public UnityEvent UnknownEvent;

    string RequestStatus = "";

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

    }

    public void InvokeLeaderboard()
    {
        StartCoroutine(ExecuteLeaderboard());
    }

    IEnumerator ExecuteLeaderboard()
    {
        WWWForm form = new WWWForm();
        form.AddField(FieldUsername, Username.text);
        form.AddField(FieldScore, Score.text);

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
                LeaderboardSuccessEvent.Invoke();
            }
            else if (RequestStatus == LinearProgressionAuthApache.USERS_EXISTS)
            {
                LeaderboardFailureEvent.Invoke();
            }
            else
            {
                UnknownEvent.Invoke();
            }
        }
    }

}
