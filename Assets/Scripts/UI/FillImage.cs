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
            var count = _saveAndLoad.FillPercent();
            switch (count)
            {
                case 0: 
                    gameObject.SetActive(false);
                    break;
                case -1:
                    _image.fillAmount = 0;
                    break;
                default:
                    _image.fillAmount = _saveAndLoad.GetCurrentOpening();
                    break;
            }
        }

        public override void Init()
        {
            if(_image)_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        }

        public override async void Animate()
        {
            try
            {
                _inventory.AddPercents(_fillAmount);
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
                if(_saveAndLoad.IsOpenedWeapon(_saveAndLoad.GetCurrentOpening()) && _saveAndLoad.GetCurrentOpening() != 0)
                {
                    WeaponTool tool = _inventory.Get(_saveAndLoad.GetCurrentOpening());
                    tool.SetParent(transform);
                    tool.transform.localScale = Vector3.one*500;
                    tool.transform.localPosition = Vector3.zero;
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