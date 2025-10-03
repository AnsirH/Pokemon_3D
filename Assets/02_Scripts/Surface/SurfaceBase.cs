using Pokemon3D.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Surface
{
    public enum ESurfaceType
    {
        Ground,
        TallGrass
    }

    public class SurfaceBase : MonoBehaviour
    {
        [Header("variables")]
        [SerializeField]
        ESurfaceType surfaceType;

        [Header("references")]
        [SerializeField]
        AudioClip[] surfaceSounds;

        public AudioClip SurfaceSound
        {
            get
            {
                return surfaceSounds[Random.Range(0, surfaceSounds.Length)];
            }
        }

        public virtual void ExecuteSurfaceEvent(PlayerController player)
        {

        }
    }
}