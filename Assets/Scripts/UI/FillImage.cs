using System;
using System.Threading.Tasks;
using Core.SaveAndLoadEssence;
using DG.Tweening;
using PlayerEssence.WeaponEssence;
using UnityEngine;
using UnityEngine.UI;
using WeaponsEssence;
using Zenject;
using Animation = UI.SequenceEssence.Animation;

namespace UI
{
    public class FillImage : Animation
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fillSpeed = 2f;
        [SerializeField] private float _fillAmount = 0.25f;

        [Inject] private WeaponInventory _inventory;
        [Inject] private ISaveAndLoadSystem _saveAndLoad;

        private void Awake()
        {
            
        }

        public override void Init()
        {
            if(_image)_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
            _image.fillAmount = 0;
        }

        public override async void Animate()
        {
            try
            {
                _inventory.AddPercents(_fillAmount);
                var count = _saveAndLoad.FillPercent();
                Debug.Log(_saveAndLoad.FillPercent());
                _image.fillAmount = 0;
                switch (count)
                {
                    case 0: 
                        _image.fillAmount = 0;
                        break;
                    case -1:
                        _image.fillAmount = 0;
                        break;
                }
                
                transform.DORewind ();
                transform.DOPunchScale (new Vector3 (1, 1, 1), .1f);
                _image.DOFade(1, .25f);
                float fullFill = _image.fillAmount + _fillAmount;
                await Task.Delay(500);
                if(_saveAndLoad.FillPercent() >= 1)
                {
                    Debug.Log($"create weapon");
                    WeaponTool tool = _inventory.Get(_saveAndLoad.GetCurrentOpening());
                    tool.SetParent(transform);
                    tool.transform.localScale = Vector3.one*500;
                    tool.transform.localPosition = Vector3.zero;
                }
                while (_image.fillAmount <= _saveAndLoad.FillPercent() )
                {
                    _image.fillAmount += _fillSpeed * Time.deltaTime;
                    await Task.Yield();
                }
                _image.fillAmount = _saveAndLoad.FillPercent();
            }
            catch (Exception e)
            {
                Debug.Log(e);
                // ignored
            }
        }
    }
}