using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Recording
{
    public class CSVWriter : MonoBehaviour
    {
        StreamWriter sw;
        public RightHandRecorder rightTracker;
        public LeftHandRecorder leftTracker;
        public VideoCapturing videoCapturing;

        private void Start()
        {
            string filename = string.Format("Test_{0}.csv", DateTime.Now.ToString("yyyyMMdd-HHmmss"));
            string filepath = System.IO.Path.Combine("C:/Users/xr/Documents/Oiwa/Recorded/CSV/OiwaKazuki", filename);
            FileStream fs = File.OpenWrite(filepath);
            sw = new StreamWriter(fs);

            sw.Write("Time"); sw.Write(",");

            sw.Write("Right Tracker Postion.x"); sw.Write(",");
            sw.Write("Right Tracker Postion.y"); sw.Write(",");
            sw.Write("Right Tracker Postion.z"); sw.Write(",");
            sw.Write("Right Tracker Rotation.x"); sw.Write(",");
            sw.Write("Right Tracker Rotation.y"); sw.Write(",");
            sw.Write("Right Tracker Rotation.z"); sw.Write(",");
            sw.Write("Right Tracker Velocity.x"); sw.Write(",");
            sw.Write("Right Tracker Velocity.y"); sw.Write(",");
            sw.Write("Right Tracker Velocity.z"); sw.Write(",");
            sw.Write("Right Tracker AngularVelocity.x"); sw.Write(",");
            sw.Write("Right Tracker AngularVelocity.y"); sw.Write(",");
            sw.Write("Right Tracker AngularVelocity.z"); sw.Write(",");

            sw.Write("Left Tracker Postion.x"); sw.Write(",");
            sw.Write("Left Tracker Postion.y"); sw.Write(",");
            sw.Write("Left Tracker Postion.z"); sw.Write(",");
            sw.Write("Left Tracker Rotation.x"); sw.Write(",");
            sw.Write("Left Tracker Rotation.y"); sw.Write(",");
            sw.Write("Left Tracker Rotation.z"); sw.Write(",");
            sw.Write("Left Tracker Velocity.x"); sw.Write(",");
            sw.Write("Left Tracker Velocity.y"); sw.Write(",");
            sw.Write("Left Tracker Velocity.z"); sw.Write(",");
            sw.Write("Left Tracker AngularVelocity.x");
            sw.Write("Left Tracker AngularVelocity.y");
            sw.Write("Left Tracker AngularVelocity.z");

            sw.WriteLine();
        }

        public void StartWriting()
        {
            
        }

        // Update is called once per frame
        public void Update()
        {
            if (videoCapturing.isRecording)
            {
                sw.Write(DateTime.Now.ToString("yyyyMMdd-HHmmssfff")); sw.Write(",");

                sw.Write(rightTracker.RightHandPosition.x); sw.Write(",");
                sw.Write(rightTracker.RightHandPosition.y); sw.Write(",");
                sw.Write(rightTracker.RightHandPosition.z); sw.Write(",");
                sw.Write(rightTracker.RightHandRotation.x); sw.Write(",");
                sw.Write(rightTracker.RightHandRotation.y); sw.Write(",");
                sw.Write(rightTracker.RightHandRotation.z); sw.Write(",");
                sw.Write(rightTracker.RightHandVelocity.x); sw.Write(",");
                sw.Write(rightTracker.RightHandVelocity.y); sw.Write(",");
                sw.Write(rightTracker.RightHandVelocity.z); sw.Write(",");
                sw.Write(rightTracker.RightHandAngularVelocity.x); sw.Write(",");
                sw.Write(rightTracker.RightHandAngularVelocity.y); sw.Write(",");
                sw.Write(rightTracker.RightHandAngularVelocity.z); sw.Write(",");

                sw.Write(leftTracker.LeftHandPosition.x); sw.Write(",");
                sw.Write(leftTracker.LeftHandPosition.y); sw.Write(",");
                sw.Write(leftTracker.LeftHandPosition.z); sw.Write(",");
                sw.Write(leftTracker.LeftHandRotation.x); sw.Write(",");
                sw.Write(leftTracker.LeftHandRotation.y); sw.Write(",");
                sw.Write(leftTracker.LeftHandRotation.z); sw.Write(",");
                sw.Write(leftTracker.LeftHandVelocity.x); sw.Write(",");
                sw.Write(leftTracker.LeftHandVelocity.y); sw.Write(",");
                sw.Write(leftTracker.LeftHandVelocity.z); sw.Write(",");
                sw.Write(leftTracker.LeftHandAngularVelocity.x); sw.Write(",");
                sw.Write(leftTracker.LeftHandAngularVelocity.y); sw.Write(",");
                sw.Write(leftTracker.LeftHandAngularVelocity.z);

                sw.WriteLine();
            }
        }

        public void TestWrite()
        {
            sw.Write(DateTime.Now.ToString("yyyyMMdd-hhmmssfff")); sw.Write(",Test");
            sw.WriteLine();
        }

        public void EndWriting()
        {
            sw.Close();
            Debug.Log("Closing csv File");
        }
    }
}