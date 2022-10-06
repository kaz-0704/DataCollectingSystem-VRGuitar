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
    private bool isCamera = true;

    void Start()
    {
        CameraOn();
    }

    public void CameraOn()
    {
        isCamera = true;
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    public void CameraOff()
    {
        isCamera = false;
        webcamTexture.Stop();
    }
}