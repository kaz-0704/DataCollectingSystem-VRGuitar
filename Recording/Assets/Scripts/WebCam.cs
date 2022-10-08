using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// WebÉJÉÅÉâ
public class WebCam : MonoBehaviour
{
    int width = 1920;
    int height = 1080;
    int fps = 30;
    WebCamTexture webcamTexture;

    void Start()
    {
        CameraOn();
    }

    public void CameraOn()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    public void CameraOff()
    {
        webcamTexture.Stop();
    }
}