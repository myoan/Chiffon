using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiffonPhotoButton : MonoBehaviour
{

    public void OnClick()
    {
        Debug.Log("OnClick!!");
        WebCamera camera = GameObject.Find("WebCamQuad").GetComponent<WebCamera>();
        byte[] png = camera.SnapShot();
        GameObject.Find("ConnectButton").GetComponent<SocketClient>().Upload(png);
        camera.Play();
    }
}
