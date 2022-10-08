using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Linq;
using UnityEngine.Windows.WebCam;

namespace Recording
{
    public class VideoCapturing : MonoBehaviour
    {
        VideoCapture m_VideoCapture = null;
        public bool isRecording = false;

        public Metronome metronome;

        public WebCam webCam;

        void Start()
        {

        }

        void Update()
        {
            if ((m_VideoCapture == null || !m_VideoCapture.IsRecording) && Input.GetKeyDown(KeyCode.S))
            {
                webCam.CameraOff();
                metronome.OnMetronome();
            }

            if (m_VideoCapture == null || !m_VideoCapture.IsRecording)
            {
                return;
            }

            if (m_VideoCapture.IsRecording && Input.GetKeyDown(KeyCode.D))
            {
                metronome.OffMetronome();
                StopVideoCapture();
            }
        }

        public void StartVideoCaptureTest()
        {
            m_VideoCapture = null;
            isRecording = true;
            Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
            Debug.Log(cameraResolution);

            float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
            Debug.Log(cameraFramerate);

            VideoCapture.CreateAsync(false, delegate (VideoCapture videoCapture)
            {
                if (videoCapture != null)
                {
                    m_VideoCapture = videoCapture;
                    Debug.Log("Created VideoCapture Instance!");

                    CameraParameters cameraParameters = new CameraParameters();
                    cameraParameters.hologramOpacity = 0.0f;
                    cameraParameters.frameRate = cameraFramerate;
                    cameraParameters.cameraResolutionWidth = cameraResolution.width;
                    cameraParameters.cameraResolutionHeight = cameraResolution.height;
                    cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

                    m_VideoCapture.StartVideoModeAsync(cameraParameters,
                                                       VideoCapture.AudioState.MicAudio,
                                                       OnStartedVideoCaptureMode);
                }
                else
                {
                    Debug.LogError("Failed to create VideoCapture Instance!");
                }
            });
        }

        void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
        {
            Debug.Log("Started Video Capture Mode!");
            string timeStamp = DateTime.Now.ToString().Replace("/", "").Replace(":", "");
            string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
            string filepath = System.IO.Path.Combine("C:/Users/xr/Documents/Oiwa/RecordedVideo", filename);
            filepath = filepath.Replace("/", @"\");
            m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
        }

        void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
        {
            Debug.Log("Stopped Video Capture Mode!");
        }

        void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
        {
            Debug.Log("Started Recording Video!");
        }

        void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
        {
            Debug.Log("Stopped Recording Video!");
            m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
        }

        public void StopVideoCapture()
        {
            isRecording = false;
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        }
    }

//#if UNITY_EDITOR
//    [CustomEditor(typeof(VideoCapturing))]

//    public class RecordingEditor : Editor
//    {
//        VideoCapturing videoCapturing;
//        public override void OnInspectorGUI()
//        {
//            RecordingEditor recordingEditor = target as RecordingEditor;

//            using(new EditorGUI.DisabledGroupScope(!Application.isPlaying))
//            {
//                if (GUILayout.Button("Start")) {
//                    videoCapturing.metronome.OnMetronome(); 
//                }
//                if (GUILayout.Button("Stop")) {
//                    videoCapturing.metronome.OffMetronome();
//                    videoCapturing.StopVideoCapture();
//                }
//            }
//            base.OnInspectorGUI();
//        }
//    }
//#endif
}