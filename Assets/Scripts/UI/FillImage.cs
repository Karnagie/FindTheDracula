using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Animation = UI.SequenceEssence.Animation;

namespace UI
{
    public class FillImage : Animation
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fillSpeed = 2f;
        [SerializeField] private float _fillAmount = 0.4f;
        
        public override void Init()
        {
            if(_image)_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        }

        public override async void Animate()
        {
            try
            {
                transform.DORewind ();
                transform.DOPunchScale (new Vector3 (1, 1, 1), .1f);
                _image.DOFade(1, .25f);
                float fullFill = _image.fillAmount + _fillAmount;
                await Task.Delay(500);
                while (_image.fillAmount <= fullFill)
                {
                    _image.fillAmount += _fillSpeed * Time.deltaTime;
                    await Task.Yield();
                }

                _image.fillAmount = fullFill;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                // ignored
            }
        }
    }
}