using System;
using UnityEngine;

namespace AliveEssence
{
    public class Vampire : AliveObject
    {
        [SerializeField] private ParticleSystem _burn;

        public void Burn()
        {
            _burn.Play();
        }
        
        public void StopBurning()
        {
            _burn.Stop();
        }
    }
}