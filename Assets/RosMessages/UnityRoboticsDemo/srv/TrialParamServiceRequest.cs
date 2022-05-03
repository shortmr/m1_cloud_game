//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class TrialParamServiceRequest : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/TrialParamService";
        public override string RosMessageName => k_RosMessageName;

        public double[] bar_heights;
        //  list of obstacle bar heights
        public bool social_flag;
        //  determines if both trajectories should be displayed on screen

        public TrialParamServiceRequest()
        {
            this.bar_heights = new double[0];
            this.social_flag = false;
        }

        public TrialParamServiceRequest(double[] bar_heights, bool social_flag)
        {
            this.bar_heights = bar_heights;
            this.social_flag = social_flag;
        }

        public static TrialParamServiceRequest Deserialize(MessageDeserializer deserializer) => new TrialParamServiceRequest(deserializer);

        private TrialParamServiceRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.bar_heights, sizeof(double), deserializer.ReadLength());
            deserializer.Read(out this.social_flag);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.WriteLength(this.bar_heights);
            serializer.Write(this.bar_heights);
            serializer.Write(this.social_flag);
        }

        public override string ToString()
        {
            return "TrialParamServiceRequest: " +
            "\nbar_heights: " + System.String.Join(", ", bar_heights.ToList()) +
            "\nsocial_flag: " + social_flag.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
