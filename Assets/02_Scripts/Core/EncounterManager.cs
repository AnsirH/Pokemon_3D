using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Singleton;
using UnityEngine.Rendering.PostProcessing;
using Pokemon3D.Sound;
using Pokemon3D.Pokemon;
using Pokemon3D.Enum;
using UnityEngine.SceneManagement;
using System;

namespace Pokemon3D.Core
{
    public class EncounterManager : Singleton<EncounterManager>
    {
        [Header("References")]
        [SerializeField] PostProcessVolume postProcessVolume;

        [Header("Variables")]
        [SerializeField] AnimationCurve cameraEffectCurve;
        [SerializeField] float duration;
        [SerializeField] int blinkCount;

        // Variables
        LensDistortion lensDistortion;
        AutoExposure autoExposure;
        ChromaticAberration chromaticAberration;

        protected override void Awake()
        {
            base.Awake();
            lensDistortion = postProcessVolume.profile.GetSetting<LensDistortion>();
            autoExposure = postProcessVolume.profile.GetSetting<AutoExposure>();
            chromaticAberration = postProcessVolume.profile.GetSetting<ChromaticAberration>();

            SceneManager.sceneLoaded += OnSceneLoadedHandler;
        }

        private void OnSceneLoadedHandler(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "Battle Scene")
            {
                lensDistortion.intensity.value = 0;
                chromaticAberration.intensity.value = 0;

                lensDistortion.enabled.value = false;
                chromaticAberration.enabled.value = false;

                BattleSystem.BattleSystem.Instance.Initialize();
                StartCoroutine(BattleStartProduction());
            }            
        }

        IEnumerator BattleStartProduction()
        {
            float timer = 0.0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                autoExposure.keyValue.value = Mathf.Lerp(15, 1, timer / duration);
                yield return null;
            }

            BattleSystem.BattleSystem.Instance.StartBattle();
        }

        public void EncounterPokemon(PokemonData enemyPokemonData, BattleOpponentType battleType)
        {
            GameManager.Instance.Player.SetCanMove(false);
            GameFlowManager.Instance.SetBattleData(enemyPokemonData, battleType);
            SoundManager.Instance.PlayEncounterMusic();
            StartCoroutine(EncounterProduction());
        }

        IEnumerator EncounterProduction()
        {
            for (int i = 0; i < blinkCount; ++i)
            {
                yield return new WaitForSeconds(0.2f);
                autoExposure.keyValue.value = 0.5f;
                yield return new WaitForSeconds(0.2f);
                autoExposure.keyValue.value = 1.0f;
            }

            lensDistortion.enabled.value = true;
            chromaticAberration.enabled.value = true;
            float timer = 0.0f;
            float value = 0.0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                value = cameraEffectCurve.Evaluate(timer / duration);
                lensDistortion.intensity.value = value * -100.0f;
                chromaticAberration.intensity.value = value;
                
                yield return null;
            }

            timer = 0.0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                autoExposure.keyValue.value = Mathf.Lerp(1, 15, timer / duration);
                yield return null;
            }

            GameFlowManager.Instance.MoveToBattleScene();
        }
    }
}