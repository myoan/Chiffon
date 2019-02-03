using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    private WebSocket ws;
    // Start is called before the first frame update
    void Start()
    {
        var url = "ws://localhost:8765";
        Debug.Log("Connect to " + url);
        ws = new WebSocket(url);
        ws.OnOpen += (sender, e) => Debug.Log ("Open");
        ws.OnMessage += (sender, e) => Debug.Log("get data: " + e.Data);
        ws.OnError += (sender, e) => Debug.Log(e.Message);
        ws.OnClose += (sender, e) => Debug.Log("close");
        ws.Connect();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Connect()
    {
        Debug.Log("Send yoan");
        ws.Send("yoan");
    }
}
