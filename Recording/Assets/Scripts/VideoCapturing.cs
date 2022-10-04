using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.WebCam;

namespace Recording
{
    public class VideoCapturing : MonoBehaviour
    {
        static readonly float MaxRecordingTime = 5.0f;

        VideoCapture m_VideoCapture = null;
        float m_stopRecordingTimer = float.MaxValue;
        public bool isRecording = false;

        public Metronome metronome;

        void Start()
        {
            StartVideoCaptureTest();
        }

        void Update()
        {
            //if ((m_VideoCapture == null || !m_VideoCapture.IsRecording) && Input.GetKeyDown(KeyCode.S))
            //{
            //    metronome.OnMetronome();
            //}

            if (m_VideoCapture == null || !m_VideoCapture.IsRecording)
            {
                return;
            }

            if (m_VideoCapture.IsRecording && Input.GetKeyDown(KeyCode.D))
            {
                metronome.OffMetronome();
                //StopVideoCapture();
                m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
            }

            if (Time.time > m_stopRecordingTimer && isRecording)
            {
                m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
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
            string timeStamp = Time.time.ToString().Replace(".", "").Replace(":", "");
            string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
            string filepath = System.IO.Path.Combine("C:/Users/kazu3/Unity/RecordedVideo", filename);
            filepath = filepath.Replace("/", @"\");
            m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
        }

        void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
        {
            isRecording = false;
            Debug.Log("Stopped Video Capture Mode!");
        }

        void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
        {
            Debug.Log("Started Recording Video!");
            //Debug.Log("******************\n" + m_VideoCapture.IsRecording + "\n********************");
            m_stopRecordingTimer = Time.time + MaxRecordingTime;
        }

        void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
        {
            Debug.Log("Stopped Recording Video!");
            m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
        }

        public void StopVideoCapture()
        {
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        }
    }
}