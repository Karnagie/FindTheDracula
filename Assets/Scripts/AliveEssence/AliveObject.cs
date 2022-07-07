using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AliveEssence
{
    public class AliveObject : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Transform _heart;
        [SerializeField] private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            foreach (var rigidbody1 in _rigidbodies)
            {
                rigidbody1.isKinematic = true;
            }
        }

        public Vector3 Heart => _heart.position;

        public Action OnKill;

        public void Die()
        {
            OnKill?.Invoke();
            _animator.enabled = false;
            foreach (var rigidbody1 in _rigidbodies)
            {
                rigidbody1.isKinematic = false;
                rigidbody1.AddForce(new Vector3(Random.Range(300, 300), Random.Range(600, 800), Random.Range(300, 300)));
            }
        }
    }
}