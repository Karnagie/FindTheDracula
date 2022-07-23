using System;
using UnityEngine;

namespace UI
{
    public class FillObject : MonoBehaviour
    {
        [SerializeField] private Renderer[] _renderers;
        
        private static readonly int F = Shader.PropertyToID("Float");

        public void Fill(float percent)
        {
            gameObject.SetActive(true);
            Debug.Log(percent);
            foreach (var renderer in _renderers)
            {
                renderer.sharedMaterial.SetFloat("_FillAmount", percent);
            }
        }
    }
}