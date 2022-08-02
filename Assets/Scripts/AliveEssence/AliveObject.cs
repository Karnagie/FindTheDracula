﻿using System;
using System.Threading.Tasks;
using Core.BusEvents;
using Core.BusEvents.Handlers;
using DG.Tweening;
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
        [SerializeField] private AliveObject[] _runOnDeath;
        [SerializeField] private Animation _run;

        [SerializeField] private ParticleSystem[] _onDeath;
        [SerializeField] private ParticleSystem[] _onDeath1;
        [SerializeField] private ParticleSystem[] _onDeath2;
        [SerializeField] private GameObject[] _turnOffOnDead;
        [SerializeField] private GameObject[] _turnOnOnDead;
        [SerializeField] private SkinnedMeshRenderer[] _renderers;
        [SerializeField] private Texture _scaredSprite;
        
        private bool _dead;

        private void Awake()
        {
            foreach (var rigidbody1 in _rigidbodies)
            {
                rigidbody1.isKinematic = true;
            }
        }

        public Vector3 Heart => _heart.position;

        public bool Dead => _dead;

        public Action OnKill;

        public async void Die(int number, bool immediately = false)
        {
            if(!immediately)await Task.Delay(500);
            
            foreach (var animator in _animators)
            {
                Debug.Log(animator.name);
                animator.SetBool("Died", true);
                animator.SetInteger("Number", number);
                animator.applyRootMotion = true;
            }
            if(!Dead)
            {
                foreach (var run in _runOnDeath)
                {
                    run.Run();
                }
                foreach (var o in _turnOffOnDead)
                {
                    o.transform.DOScale(o.transform.localScale*0.8f, .1f);
                }
                await Task.Delay((int)(.1 * 1000));
                foreach (var o in _turnOffOnDead)
                {
                    o.SetActive(false);
                }
                foreach (var o in _turnOnOnDead)
                {
                    o.transform.localPosition = Vector3.zero;
                    o.transform.localScale *= 0.5f;
                    o.transform.DOScale(o.transform.localScale*2f, 0.2f).SetEase(Ease.OutBounce);
                    o.SetActive(true);
                }
                await Task.Delay((int)(.2 * 1000));
                if (number == 0)
                {
                    foreach (var o in _onDeath)
                    {
                        o.Play();
                    }
                }
                if (number == 1)
                {
                    foreach (var o in _onDeath1)
                    {
                        o.Play();
                    }
                }
                if (number == 2)
                {
                    foreach (var o in _onDeath2)
                    {
                        o.Play();
                    }
                }
                if(!Dead)OnKill?.Invoke();
            }
            _dead = true;
            foreach (var rigidbody1 in _rigidbodies)
            {
                rigidbody1.isKinematic = false;
                rigidbody1.AddForce(new Vector3(Random.Range(300, 300), Random.Range(600, 800), Random.Range(300, 300)));
            }
            if(_loseOnDie)
                EventBus.RaiseEvent((IGameStateHandler handler) => handler.LoseLevel());
        }

        private void Run()
        {
            if (_run != null) _run.Play();
            foreach (var renderer in _renderers)
            {
                renderer.material.mainTexture = _scaredSprite;
            }
            
            foreach (var animator in _animators)
            {
                animator.SetBool("Run", true);
            }
        }
    }
}