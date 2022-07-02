using System;
using UnityEngine;

namespace PlayerEssence.CameraEssence
{
    public class Camera
    {
        private UnityEngine.Camera _camera;
        private Texture2D _screenTexture;
        //private int height = 1024;
        //private int width = 1024;
        private int depth = 24;

        public Camera(UnityEngine.Camera camera)
        {
            _camera = camera;
        }
        
        public Sprite CaptureScreen(int height, int width) 
        {
            RenderTexture renderTexture = new RenderTexture(width, height, depth);
            Rect rect = new Rect(0,0,width,height);
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            _camera.targetTexture = renderTexture;
            _camera.Render();

            RenderTexture currentRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();

            _camera.targetTexture = null;
            RenderTexture.active = currentRenderTexture;
            MonoBehaviour.Destroy(renderTexture);

            Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);

            return sprite;
        }
    }
}