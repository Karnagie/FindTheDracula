using System;
using System.Collections.Generic;
using System.Linq;
using Core.InputEssence;
using Core.RaycastingEssence;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Core.RayCastingEssence
{
    public class RayCasting
    {
        private Camera _camera;
        private List<IRaycastTarget> _stash;
        private IInputSystem _input;

        public RayCasting(IInputSystem input)
        {
            _camera = Camera.main;
            _input = input;
            _input.OnEndClick += OnCastEnd;
            _stash = new List<IRaycastTarget>();
        }

        public Vector3 MousePositionInWorld => (Vector2)_camera.ScreenToWorldPoint(_input.MousePosition);

        public T Cast<T>(bool closest = false) where T : Component, IRaycastTarget
        {
            RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(_input.MousePosition));
            hits = hits.OrderBy(hit => hit.point.z).ToArray();
            
            List<T> components = new List<T>();
            foreach (var hit in hits)
            {
                if(hit.transform.TryGetComponent<T>(out var component))
                {
                    components.Add(component);
                }
            }

            if(closest){
                float minDistance = Single.MaxValue;
                T closestComponent = null;
                foreach (var component in components)
                {
                    if ((component.transform.position - MousePositionInWorld).magnitude < minDistance)
                    {
                        minDistance = (component.transform.position - MousePositionInWorld).magnitude;
                        closestComponent = component;
                    }
                }
                closestComponent.OnStartClick?.Invoke();
                _stash.Add(closestComponent);
                return closestComponent;
            } else if (components.Count > 0)
            {
                T component = components.First();
                components.First().OnStartClick?.Invoke();
                _stash.Add(component);
                return component;
            }
            else return null;
        }
        
        public T[] CastAll<T>()// where T : Component, IRaycastTarget
        {
            RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(_input.MousePosition));
            List<T> components = new List<T>();
            foreach (var hit in hits)
            {
                if(hit.transform.TryGetComponent<T>(out var component))
                {
                    //component.OnStartClick?.Invoke();
                    //_stash = component;
                    components.Add(component);
                }
            }
            return components.ToArray();
        }

        public bool Cast(ref RaycastHit hit)
        {
            RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(_input.MousePosition));
            if (hits.Length > 1)
            {
                hit = hits[0];
                return true;
            }

            return false;
        }
        
        public bool Cast(ref RaycastHit hit, int layers)
        {
            RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(_input.MousePosition), 30, layers);
            if (hits.Length > 1)
            {
                hit = hits[0];
                return true;
            }

            return false;
        }

        private void OnCastEnd(InputAction.CallbackContext ctx)
        {
            _stash?.ForEach((target => target.OnEndClick?.Invoke()));
            _stash = new List<IRaycastTarget>();
        }
    }
}