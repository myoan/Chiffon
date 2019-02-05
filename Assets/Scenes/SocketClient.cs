using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.Networking;
using System;

public class SocketClient : MonoBehaviour
{
    private WebSocket ws;
    // Start is called before the first frame update
    void Start()
    {
        /*
        var url = "ws://localhost:8765";
        Debug.Log("Connect to " + url);
        ws = new WebSocket(url);
        ws.OnOpen += (sender, e) => Debug.Log ("Open");
        ws.OnMessage += (sender, e) => Debug.Log("get data: " + e.Data);
        ws.OnError += (sender, e) => Debug.Log(e.Message);
        ws.OnClose += (sender, e) => Debug.Log("close");
        ws.Connect();
        */
    }

    public void Upload(byte[] png)
    {
        StartCoroutine(SendPNG(png));
    }

    public IEnumerator SendPNG(byte[] png)
    {
        WWWForm form = new WWWForm();
        form.AddField("data", Convert.ToBase64String (png));
 
        using(UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/photo", form)) {
            yield return www.Send();
 
            if (www.isError) {
                Debug.Log(www.error);
            } else {
                Debug.Log("Form upload complete!");
            }
        }
    }
}