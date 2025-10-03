using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Surface
{
    public class TallGrass : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] Animator anim;
        [SerializeField] Collider coll;
        [SerializeField] AudioSource audioSource;

        [Header("variables")]
        [SerializeField] AudioClip[] movementSounds;

        // properties
        public Collider Coll => coll;

        private void Awake()
        {
            if (anim == null)
                anim = GetComponent<Animator>();

            if (coll == null)
                coll = GetComponent<Collider>();

            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            anim.speed = Random.Range(0.8f, 1.1f);
        }

        public void Interact(Vector3 direction)
        {
            anim.SetFloat("DirectionX", direction.x);
            anim.SetFloat("DirectionZ", direction.z);
            anim.CrossFade("BlendTree", 0.5f, -1, 0f);
            PlayMovementSound();
        }
        
        private void PlayMovementSound()
        {
            audioSource.PlayOneShot(movementSounds[Random.Range(0, movementSounds.Length)]);
        }
    }
}