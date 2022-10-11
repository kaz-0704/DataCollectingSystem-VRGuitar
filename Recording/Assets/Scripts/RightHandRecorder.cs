using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Recording
{
    public class RightHandRecorder : MonoBehaviour
    {
        public Vector3 RightHandPosition;      //�ʒu
        public Quaternion RightHandRotationQ;  //�N�H�[�^�j�I����]���W
        public Vector3 RightHandRotation;      //�I�C���[�p
        public Vector3 RightHandVelocity;      //���x
        public Vector3 RightHandAngularVelocity;    //�p���x
        public float RightHandAngularVelocityMagnitude;

        private SteamVR_Action_Pose right_tracker = SteamVR_Actions.default_Pose;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RightHandPosition = right_tracker.GetLocalPosition(SteamVR_Input_Sources.RightHand);
            RightHandRotationQ = right_tracker.GetLocalRotation(SteamVR_Input_Sources.RightHand);
            RightHandRotation = RightHandRotationQ.eulerAngles;
            RightHandVelocity = right_tracker.GetLastVelocity(SteamVR_Input_Sources.RightHand);
            RightHandAngularVelocity = right_tracker.GetAngularVelocity(SteamVR_Input_Sources.RightHand);
            RightHandAngularVelocityMagnitude = CalcMagnitude(RightHandAngularVelocity);
            //right_tracker.GetAngularVelocity(SteamVR_Input_Sources.RightHand);

            //Debug.Log("右手の角速度："+RightHandAngularVelocity);
        }

        float CalcMagnitude(Vector3 vector)
        {
            float temp = Mathf.Pow(vector[0], 2) + Mathf.Pow(vector[1], 2) + Mathf.Pow(vector[2], 2);
            return Mathf.Sqrt(temp);
        }
    }
}
