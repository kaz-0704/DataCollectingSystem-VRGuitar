using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Recording
{
    public class LeftHandRecorder : MonoBehaviour
    {
        public Vector3 LeftHandPosition;      //�ʒu
        public Quaternion LeftHandRotationQ;  //�N�H�[�^�j�I����]���W
        public Vector3 LeftHandRotation;      //�I�C���[�p
        public Vector3 LeftHandVelocity;      //���x
        public Vector3 LeftHandAngularVelocity;    //�p���x
        public float LeftHandAngularVelocityMagnitude;

        private SteamVR_Action_Pose left_tracker = SteamVR_Actions.default_Pose;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            LeftHandPosition = left_tracker.GetLocalPosition(SteamVR_Input_Sources.LeftHand);
            LeftHandRotationQ = left_tracker.GetLocalRotation(SteamVR_Input_Sources.LeftHand);
            LeftHandRotation = LeftHandRotationQ.eulerAngles;
            LeftHandVelocity = left_tracker.GetLastVelocity(SteamVR_Input_Sources.LeftHand);
            LeftHandAngularVelocity = left_tracker.GetAngularVelocity(SteamVR_Input_Sources.LeftHand);
            LeftHandAngularVelocityMagnitude = CalcMagnitude(LeftHandAngularVelocity);

            //Debug.Log("左手の角速度："+LeftHandAngularVelocity);
        }

        float CalcMagnitude(Vector3 vector)
        {
            float temp = Mathf.Pow(vector[0], 2) + Mathf.Pow(vector[1], 2) + Mathf.Pow(vector[2], 2);
            return Mathf.Sqrt(temp);
        }
    }
}
