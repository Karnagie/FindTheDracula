using System;
using System.Collections.Generic;
using Core.RayCastingEssence;
using UnityEngine;
using VampireEssence;
using Zenject;

namespace PlayerEssence.FlashlightEssence
{
    public class FlashlightLight :  MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        
        [Inject] private RayCasting _rayCasting;

        private List<Vampire> _cache = new List<Vampire>();
        private Vector3 _defaultScale;
        private Vector3 _defaultPosition;

        private bool _isWorking;

        private void Awake()
        {
            _defaultScale = transform.localScale;
            _defaultPosition = transform.localPosition;
        }

        private void Update()
        {
            RaycastHit hit = new RaycastHit();
            Vector3 scale = _defaultScale;
            Vector3 position = _defaultPosition;
            if (_rayCasting.Cast(ref hit, LayerMask.GetMask("Stuff", "Vampire", "Layer1", "Layer2")))
            {
                scale.y = Vector3.Distance(_pivot.position, hit.point);
            }

            position.z = scale.y;
            transform.localScale = scale;
            transform.localPosition = position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Vampire vampire) && _isWorking)
            {
                vampire.Burn();
                _cache.Add(vampire);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Vampire vampire) && _isWorking)
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
            _isWorking = false;
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
            _isWorking = true;
        }
    }
}