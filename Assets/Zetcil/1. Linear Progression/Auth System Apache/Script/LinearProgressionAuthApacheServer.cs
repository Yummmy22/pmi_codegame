using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Xml;
using System.Diagnostics;

public class LinearProgressionAuthApacheServer : MonoBehaviour
{
    public enum CServerType
    {
        None, StaticServer, DynamicServer
    }

    [Header("Server Settings")]
    public CServerType ServerType;
    public string Apache = "http://localhost";

    [Header("XML Settings")]
    public string MainXML = "/config/application.xml";
    public string NodeXML = "/server/location";

    string ServerDebug()
    {
        return (ServerType == CServerType.StaticServer ? "Static Server: " : "Dynamic Server: ");
    }

    IEnumerator Start()
    {
        if (ServerType == CServerType.StaticServer)
        {
            SaveServerLocation(Apache);
        }
        if (ServerType == CServerType.DynamicServer)
        {
            // Path menuju file XML di folder StreamingAssets
            string filePath = Application.streamingAssetsPath + MainXML;

            // Buat UnityWebRequest untuk memuat file XML
            UnityWebRequest www = UnityWebRequest.Get(filePath);

            // Kirim permintaan dan tunggu respons
            yield return www.SendWebRequest();

            // Cek jika ada error
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                UnityEngine.Debug.Log("Failed to load XML: " + www.error);
            }
            else
            {
                // Mendapatkan isi XML dari respons
                string xmlText = www.downloadHandler.text;

                // Proses XML
                ProcessXML(xmlText);
            }
        }
    }

    void ProcessXML(string xmlText)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlText);
        XmlNode serverNode = xmlDoc.SelectSingleNode(NodeXML);
        string serverValue = serverNode.InnerText.Trim();
        Apache = serverValue;
        SaveServerLocation(serverValue);
    }

    void SaveServerLocation(string server)
    {
        PlayerPrefs.SetString(LinearProgressionAuthApache.CURRENT_SERVER, server);
        UnityEngine.Debug.Log(ServerDebug() + server);
    }
}
