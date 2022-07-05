using System;
using System.Collections;
using RootMotion.Dynamics;
using UnityEngine;

namespace VampireEssence
{
    public class DyingRagdoll : MonoBehaviour
    {
        [SerializeField] private PuppetMaster _puppetMaster;
        
        [Tooltip("The speed of fading out PuppetMaster.pinWeight.")]
        [SerializeField] private float fadeOutPinWeightSpeed = 5f;

        [Tooltip("The speed of fading out PuppetMaster.muscleWeight.")]
        [SerializeField] private float fadeOutMuscleWeightSpeed = 5f;

        [Tooltip("The muscle weight to fade out to.")]
        [SerializeField] private float deadMuscleWeight = 0.3f;

        public void Die()
        {
            if (_puppetMaster != null) {
                StopAllCoroutines();
                StartCoroutine(FadeOutPinWeight());
                StartCoroutine(FadeOutMuscleWeight());
            }
        }
        
        // Fading out puppetMaster.pinWeight to zero
        private IEnumerator FadeOutPinWeight() {
            while (_puppetMaster.pinWeight > 0f) {
                _puppetMaster.pinWeight = Mathf.MoveTowards(_puppetMaster.pinWeight, 0f, Time.deltaTime * fadeOutPinWeightSpeed);
                yield return null;
            }
        }

        // Fading out puppetMaster.muscleWeight to deadMuscleWeight
        private IEnumerator FadeOutMuscleWeight() {
            while (_puppetMaster.muscleWeight > 0f) {
                _puppetMaster.muscleWeight = Mathf.MoveTowards(_puppetMaster.muscleWeight, deadMuscleWeight, Time.deltaTime * fadeOutMuscleWeightSpeed);
                yield return null;
            }
        }
    }
}