using System;
using System.Collections;
using Core.InputEssence;
using PlayerEssence.ToolsEssence;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlayerEssence.CameraEssence
{
    public class CameraTool : Tool
    {
        [SerializeField] private Image _image;
        [SerializeField] private UnityEngine.Camera _viewCamera;
        
        private Camera _camera;

        [Inject] private IInputSystem _input;

        protected override void Awake()
        {
            base.Awake();
            //_input.OnStartClick += ctx => TakePhoto();
            _camera = new Camera(_viewCamera);
        }

        [ContextMenu("Take photo")]
        private void TakePhoto()
        {
            _image.sprite = _camera.CaptureScreen(1024, 1024);
        }

        // public override void Press(Vector3 direction)
        // {
        //     
        // }
        //
        // public override void Click(Vector3 direction)
        // {
        //     _rotator.LookAt(direction);
        //     TakePhoto();
        // }
    }
}