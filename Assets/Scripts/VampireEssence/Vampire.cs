using UnityEngine;

namespace VampireEssence
{
    public class Vampire : MonoBehaviour
    {
        [SerializeField] private Animator[] _animators;
        [SerializeField] private ParticleSystem _burn;
        [SerializeField] private Transform _heart;
        
        private static readonly int Died = Animator.StringToHash("Died");

        public Vector3 Heart => _heart.position;

        public void Burn()
        {
            _burn.Play();
        }
        
        public void StopBurning()
        {
            _burn.Stop();
        }

        public void Die()
        {
            foreach (var animator in _animators)    
            {
                animator.SetBool(Died, true);   
            }
        }
    }
}