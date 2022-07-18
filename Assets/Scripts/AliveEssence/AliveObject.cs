using System;
using Core.BusEvents;
using Core.BusEvents.Handlers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AliveEssence
{
    public class AliveObject : MonoBehaviour
    {
        [SerializeField] protected Animator[] _animators;
        [SerializeField] protected Transform _heart;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private bool _loseOnDie;

        [SerializeField] private ParticleSystem[] _onDeath;
        [SerializeField] private GameObject[] _turnOffOnDead;
        [SerializeField] private GameObject[] _turnOnOnDead;

        private bool _dead;

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
            if(!_dead)
            {
                foreach (var o in _turnOffOnDead)
                {
                    o.SetActive(false);
                }
                foreach (var o in _turnOnOnDead)
                {
                    o.SetActive(true);
                }
                foreach (var o in _onDeath)
                {
                    o.Play();
                }
                OnKill?.Invoke();
            }
            _dead = true;
            foreach (var animator in _animators)
            {
                animator.SetBool("Died", true);
            }
            foreach (var rigidbody1 in _rigidbodies)
            {
                rigidbody1.isKinematic = false;
                rigidbody1.AddForce(new Vector3(Random.Range(300, 300), Random.Range(600, 800), Random.Range(300, 300)));
            }
            if(_loseOnDie)
                EventBus.RaiseEvent((IGameStateHandler handler) => handler.LoseLevel());
        }
    }
}