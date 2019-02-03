using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiffonPhotoButton : MonoBehaviour
{

    public void OnClick()
    {
        Debug.Log("OnClick!!");
        GameObject.Find("WebCamQuad").GetComponent<WebCamera>().SnapShot();
    }
}
