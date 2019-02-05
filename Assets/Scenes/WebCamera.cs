using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamera : MonoBehaviour
{
    //このクラスをウェブカメラの映像をテクスチャとして貼り付けるオブジェクトに適用する
    private WebCamTexture webcamtex;
    // Use this for initialization

    void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamtex = new WebCamTexture(devices[0].name, 1024, 1024, 30);
        GetComponent<Renderer> ().material.mainTexture = webcamtex;
        webcamtex.Play();
    }

    public byte[] SnapShot() {
        Debug.Log("SnapShot!!");
        Color32[] color32 = webcamtex.GetPixels32();
        Texture2D texture = new Texture2D(webcamtex.width, webcamtex.height);
        GameObject.Find("WebCamQuad").GetComponent<Renderer>().material.mainTexture = texture;
        texture.SetPixels32(color32);
        texture.Apply();
        return texture.EncodeToPNG();
    }
}
