using System;
using UnityEngine;

namespace SnackersUI
{
    public class TestShow : MonoBehaviour
    {
        [SerializeField] private TweenAnimatedUIElement _panel;

        private void Start()
        {
            _panel.Show();
        }
    }
}