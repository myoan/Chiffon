using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.Networking;
using System;

[Serializable]
public class PhotoRequest {
    public string data;
}

[Serializable]
public class PhotoResponse {
    public string id;
    public bool status;
}
[Serializable]
public class StatusResponse {
    public bool status;
    public int x;
    public int y;
}

public class SocketClient : MonoBehaviour
{
    private WebSocket ws;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Upload(byte[] png)
    {
        StartCoroutine(SendPNG(png));
    }

    public IEnumerator SendPNG(byte[] png)
    {
        Debug.Log("SendPNG");
        PhotoRequest req = new PhotoRequest();
        req.data = Convert.ToBase64String(png);
        string json = JsonUtility.ToJson(req);
        Debug.Log(json);
        Debug.Log(json.Length);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        // var request = new UnityWebRequest("http://localhost:3000/photo", "POST");
        var request = new UnityWebRequest("http://chiffon.yoanm.tk/photo", "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("sending");
        yield return request.Send();
        Debug.Log(request.downloadHandler.text);
        PhotoResponse resp = JsonUtility.FromJson<PhotoResponse>(request.downloadHandler.text);
        Debug.Log("id: " + resp.id);
        Debug.Log("status: " + resp.status);
        yield return CheckStatus(resp.id);
    }

    public IEnumerator CheckStatus(string ts)
    {
        Debug.Log("CheckStatus: " + ts);
        while (true) {
            // var request = new UnityWebRequest("http://localhost:3000/status?ts=" + ts, "GET");
            var request = new UnityWebRequest("http://chiffon.yoanm.tk/status?ts=" + ts, "GET");
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return new WaitForSeconds(0.5F);
            yield return request.Send();
            StatusResponse resp = JsonUtility.FromJson<StatusResponse>(request.downloadHandler.text);
            Debug.Log("status: " + resp.status);
            Debug.Log("x: " + resp.x);
            Debug.Log("y: " + resp.y);
            if (resp.status == true) {
                GameObject arrow = GameObject.Find("ArrowQuad");
                LineRenderer renderer = arrow.AddComponent<LineRenderer>();
                GameObject go = GameObject.Find("WebCamQuad");
                renderer.startWidth = 10f;
                renderer.endWidth = 10f;
                renderer.positionCount = 2;
                Vector3 center = new Vector3(arrow.transform.position.x, arrow.transform.position.y, 0);
                float r = Mathf.Sqrt((resp.x * resp.x) + (resp.y * resp.y));
                float x = (float)resp.x / r * 500;
                float y = (float)resp.x / r * 500;
                renderer.SetPosition(0, center);
                renderer.SetPosition(1, new Vector3(center.x + x, center.y + y, 0f));
                go.SetActive(false);
                yield return new WaitForSeconds(2.0F);
                go.SetActive(true);
                Destroy(renderer);
                yield break;
            }
        }
    }
}