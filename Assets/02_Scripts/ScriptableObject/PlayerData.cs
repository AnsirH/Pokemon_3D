using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.ScriptableObj
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "SO/Create New Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("이동")]
        public float RotationSpeed = 1080;
        public float WalkSpeed = 5.0f;
        public float RunSpeed = 10.0f;
        public float Acceleration = 10.0f;
        public float Deceleration = 10.0f;
    }
}