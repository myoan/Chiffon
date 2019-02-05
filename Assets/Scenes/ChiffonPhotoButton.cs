using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiffonPhotoButton : MonoBehaviour
{

    public void OnClick()
    {
        Debug.Log("OnClick!!");
        var png = GameObject.Find("WebCamQuad").GetComponent<WebCamera>().SnapShot();
        GameObject.Find("ConnectButton").GetComponent<SocketClient>().Upload(png);
    }
}
