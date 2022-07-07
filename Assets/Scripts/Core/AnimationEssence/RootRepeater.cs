using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AnimationEssence
{
    public class RootRepeater : MonoBehaviour
    {
        [SerializeField] private Transform _mainRoot;
        [SerializeField] private List<TransformPair> _pairs;

        private List<Transform> _mainChilds = new List<Transform>();
        private List<Transform> _childs = new List<Transform>();

        private void Update()
        {
            foreach (var pair in _pairs)
            {
                pair.Repeat();
            }
        }


        private void OnValidate()
        {
            if(_mainRoot == null && _pairs.Count > 0) return;
            
            _mainChilds = new List<Transform>();
            _childs = new List<Transform>();
            
            AddChildren(transform);
            AddMainChildren(_mainRoot);

            _pairs = new List<TransformPair>();
            for (int i = 0; i < _mainChilds.Count; i++)
            {
                _pairs.Add(new TransformPair(_mainChilds[i], _childs[i]));
            }
        }

        private void AddChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                _childs.Add(child);   
                AddChildren(child);
            }
        }
        
        private void AddMainChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                _mainChilds.Add(child);
                AddMainChildren(child);
            }
        }
    }
    
    [Serializable]
    public class TransformPair
    {
        public Transform Main;
        public Transform Child;

        public TransformPair(Transform main, Transform child)
        {
            Main = main;
            Child = child;
        }

        public void Repeat()
        {
            Child.transform.position = Main.transform.position;
            Child.transform.rotation = Main.transform.rotation;
        }
    }
}