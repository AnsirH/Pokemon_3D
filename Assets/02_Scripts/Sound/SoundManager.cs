using Pokemon3D.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [Header("References")]
        [SerializeField] AudioSource unloopAudioSource;
        [SerializeField] AudioSource loopAudioSource;
        [SerializeField] AudioClip encounterMusic;
        [SerializeField] AudioClip battleLoopMusic;
        [SerializeField] AudioClip victoryMusic;

        public void PlayEncounterMusic()
        {
            unloopAudioSource.clip = encounterMusic;
            unloopAudioSource.Play();
            unloopAudioSource.loop = false;

            StartCoroutine(TranslateEncounterToBattleLoop());
        }

        IEnumerator TranslateEncounterToBattleLoop()
        {
            yield return new WaitUntil(() => unloopAudioSource.clip == encounterMusic && unloopAudioSource.time > encounterMusic.length - 0.15f);

            while (true)
            {
                loopAudioSource.clip = battleLoopMusic;
                loopAudioSource.Play();

                yield return new WaitUntil(() => loopAudioSource.clip == battleLoopMusic && loopAudioSource.time > battleLoopMusic.length - 0.1f);
            }
        }

        public void PlayVictoryMusic()
        {
            StopAllCoroutines();
            loopAudioSource.Stop();
            unloopAudioSource.clip = victoryMusic;
            unloopAudioSource.Play();
        }
    }
}