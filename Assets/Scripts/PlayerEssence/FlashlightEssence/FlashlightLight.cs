using System;
using System.Collections.Generic;
using UnityEngine;
using VampireEssence;

namespace PlayerEssence.FlashlightEssence
{
    public class FlashlightLight :  MonoBehaviour
    {
        private List<Vampire> _cache = new List<Vampire>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Vampire vampire))
            {
                vampire.Burn();
                _cache.Add(vampire);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Vampire vampire))
            {
                vampire.StopBurning();
                _cache.Remove(vampire);
            }
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
            foreach (var vampire in _cache)
            {
                vampire.StopBurning();
            }
            _cache.Clear();
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
        }
    }
}