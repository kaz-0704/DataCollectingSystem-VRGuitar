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
        public CSVWriter csvWriter;

        public static readonly string[] pulldownList = { 
            "Group1/植村くん", "Group1/長橋くん", "Group1/有瀧くん", "Group1/わきたくん" ,
            "Group2",
            "Group3"
        };

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

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (isRecording) { StopVideoCapture(); }
                metronome.OffMetronome();
                csvWriter.EndWriting();
            }
        }

        public void StartVideoCaptureTest()
        {
            m_VideoCapture = null;
            isRecording = true;
            csvWriter.StartWriting();
            //csvWriter.TestWrite();
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
                                                       VideoCapture.AudioState.ApplicationAndMicAudio,
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
            string timeStamp = DateTime.Now.ToString("yyyyMMdd-HHmmssfff");
            string filename = string.Format("Video_{0}.mp4", timeStamp);
            string filepath = System.IO.Path.Combine("C:/Users/xr/Documents/Oiwa/Recorded/RecordedVideo/OiwaKazuki", filename);
            filepath = filepath.Replace("/", @"\");
            //isRecording = true;
            //csvWriter.TestWrite();
            //csvWriter.StartWriting();
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
            Debug.Log("m_VideoCapture: "+(m_VideoCapture == null ? "null" : "exist"));
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        }
    }

//#if UNITY_EDITOR
//    [CustomEditor(typeof(VideoCapturing))]

//    public class RecordingEditor : Editor
//    {
//        public VideoCapturing videoCapturing;
//        public WebCam webCam;
//        public CSVWriter csvWriter;

//        public override void OnInspectorGUI()
//        {
//            RecordingEditor recordingEditor = target as RecordingEditor;

//            using (new EditorGUI.DisabledGroupScope(!Application.isPlaying))
//            {
//                if (GUILayout.Button("Start") && !videoCapturing.isRecording)
//                {
//                    webCam.CameraOff();
//                    videoCapturing.metronome.OnMetronome();
//                }
//                if (GUILayout.Button("Stop"))
//                {
//                    if (videoCapturing.isRecording) { videoCapturing.StopVideoCapture(); }
//                    videoCapturing.metronome.OffMetronome();
//                    csvWriter.EndWriting();
//                }
//                //if (GUILayout.Button("ファイル Close"))
//                //{
//                //    csvWriter.EndWriting();
//                //}
//            }
//            base.OnInspectorGUI();
//        }
//    }
//#endif
}