using Pokemon3D.Player;
using Pokemon3D.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Core
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Referenses")]
        [SerializeField] PlayerController player;

        // Properties
        public PlayerController Player => player;
    }
}