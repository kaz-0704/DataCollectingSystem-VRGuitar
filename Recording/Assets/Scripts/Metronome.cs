using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using TMPro;

namespace Recording
{
    public class Metronome : MonoBehaviour
    {
        public MidiStreamPlayer midiStreamPlayer;
        private MPTKEvent NotePlaying;
        public float bpm = 80f;
        private int preCount = 5;
        private int recordingCount = 12;

        private float timeOut;
        private float timeElapsed = 0;
        private int count = 0;
        private bool metronome = false;

        public VideoCapturing videoCapturing;
        private TextMeshProUGUI countText;
        // Start is called before the first frame update
        void Start()
        {
            timeOut = 60 / bpm;
            countText = GetComponentInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            //bpmText.text = "BPM: " + bpm;
            timeOut = 60 / bpm;
            if (metronome)
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= timeOut)
                {
                    ClickMetronome();
                    count += 1;
                    timeElapsed = timeElapsed - timeOut;
                }
            }

            if (0 < count && count < preCount) { countText.text = "" + (5 - count); }
            else if (preCount <= count && count <= recordingCount) { countText.text = "Recording..."; }
            else { countText.text = ""; }

            if (count >= preCount && !videoCapturing.isRecording)
            {
                videoCapturing.StartVideoCaptureTest();
            }

            if (count >= recordingCount && videoCapturing.isRecording)
            {
                count = 0;
                videoCapturing.StopVideoCapture();
            }
        }

        public void ClickMetronome()
        {
            midiStreamPlayer.MPTK_ChannelPresetChange(1, 113);
            NotePlaying = new MPTKEvent()
            {
                Command = MPTKCommand.NoteOn,
                Value = 72,
                Channel = 1,
                Duration = 200,
                Velocity = 100,
                Delay = 0,
            };
            midiStreamPlayer.MPTK_PlayEvent(NotePlaying);
        }

        public void OnMetronome()
        {
            Debug.Log("Metronome On");
            if (!metronome)
            {
                metronome = true;
            }
        }

        public void OffMetronome()
        {
            Debug.Log("Metronome Off");
            if (metronome)
            {
                metronome = false;
            }
        }
    }
}
