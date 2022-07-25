using System;
using System.Threading.Tasks;
using Core.SaveAndLoadEssence;
using DG.Tweening;
using PlayerEssence.ChoosableEquipmentEssence;
using PlayerEssence.ToolsEssence;
using PlayerEssence.WeaponEssence;
using TMPro;
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
        [SerializeField] private ParticleSystem[] _onGetting;
        [SerializeField] private TMP_Text _percents;

        [SerializeField] private FillObject[] _weapons;
        [SerializeField] private FillObject[] _tools;

        [Inject] private WeaponInventory _inventory;
        [Inject] private ChoosableEquipmentInventory _equipmentInventory;
        [Inject] private ISaveAndLoadSystem _saveAndLoad;


        public override void Init()
        {
            if(_image)_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
            _image.fillAmount = 0;
        }

        public override async void Animate()
        {
            Debug.Log($"start animating");
            try
            {
                Debug.Log($"Start animating {_inventory.IsAllFilled()} {_saveAndLoad.GetCurrentEquipmentOpening()}");
                int w = _saveAndLoad.IsNextWeaponOrQuestion();
                if ((w == 0 && _saveAndLoad.GetCurrentWeaponOpening() != 0 && _inventory.IsAllFilled() == false)
                    || _equipmentInventory.IsAllFilled())
                {
                    Debug.Log( _equipmentInventory.IsAllFilled());
                    _inventory.AddPercents(_fillAmount);
                    var count = _saveAndLoad.FillWeaponPercent();
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
                    //_image.DOFade(1, .25f);
                    float fullFill = _image.fillAmount + _fillAmount;
                    await Task.Delay(500);
                    if(_saveAndLoad.FillWeaponPercent() >= 1)
                    {
                        Debug.Log($"create weapon");
                        _percents.text = "100%";
                        WeaponTool tool = _inventory.Get(_saveAndLoad.GetCurrentWeaponOpening());
                        tool.SetParent(transform);
                        tool.transform.localScale = Vector3.one*500;
                        tool.transform.localPosition = Vector3.zero;
                        tool.transform.DOLocalRotate(tool.transform.localRotation.eulerAngles+new Vector3(0, 360, 0), 3, RotateMode.FastBeyond360 )
                            .SetLoops(-1)
                            .SetEase(Ease.Linear)
                            .SetRelative();
                        foreach (var system in _onGetting)
                        {
                            system.Play();
                        }
                    }
                    else
                    {
                        _weapons[_saveAndLoad.GetCurrentWeaponOpening()].Fill(_image.fillAmount);
                        while (_image.fillAmount <= _saveAndLoad.FillWeaponPercent() )
                        {
                            _image.fillAmount += _fillSpeed * Time.deltaTime;
                            string per = (_image.fillAmount*100).ToString();
                            if (per.Length > 3)
                            {
                                per = per.Remove(3, per.Length-4);
                            }
                            _percents.text = per+"%";
                            _weapons[_saveAndLoad.GetCurrentWeaponOpening()].Fill(_image.fillAmount);
                            await Task.Yield();
                        }
                        _image.fillAmount = _saveAndLoad.FillWeaponPercent();
                        _weapons[_saveAndLoad.GetCurrentWeaponOpening()].Fill(_image.fillAmount);
                    }
                }
                else if (_saveAndLoad.GetCurrentEquipmentOpening() != 0 || _inventory.IsAllFilled())
                {
                    Debug.Log(_saveAndLoad.FillEquipmentPercent());
                    _equipmentInventory.AddPercents(_fillAmount);
                    var count = _saveAndLoad.FillEquipmentPercent();
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
                    //_image.DOFade(1, .25f);
                    float fullFill = _image.fillAmount + _fillAmount;
                    await Task.Delay(500);
                    if(_saveAndLoad.FillEquipmentPercent() >= 1)
                    {
                        Debug.Log($"create Equipment");
                        _percents.text = "100%";
                        Tool tool = _equipmentInventory.Get(_saveAndLoad.GetCurrentEquipmentOpening());
                        tool.SetParent(transform);
                        tool.transform.localScale = Vector3.one*500;
                        tool.transform.localPosition = Vector3.zero;
                        tool.transform.DOLocalRotate(tool.transform.localRotation.eulerAngles+new Vector3(0, 360, 0), 3, RotateMode.FastBeyond360 )
                            .SetLoops(-1)
                            .SetEase(Ease.Linear)
                            .SetRelative();
                        foreach (var system in _onGetting)
                        {
                            system.Play();
                        }
                    }
                    else
                    {
                        _tools[_saveAndLoad.GetCurrentEquipmentOpening()].Fill(_image.fillAmount);
                        while (_image.fillAmount <= _saveAndLoad.FillEquipmentPercent() )
                        {
                            _image.fillAmount += _fillSpeed * Time.deltaTime;
                            string per = (_image.fillAmount*100).ToString();
                            if (per.Length > 3)
                            {
                                per = per.Remove(3, per.Length-4);
                            }
                            _percents.text = per+"%";
                            _tools[_saveAndLoad.GetCurrentEquipmentOpening()].Fill(_image.fillAmount);
                            await Task.Yield();
                        }
                        _image.fillAmount = _saveAndLoad.FillEquipmentPercent();
                        _tools[_saveAndLoad.GetCurrentEquipmentOpening()].Fill(_image.fillAmount);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                // ignored
            }
        }
    }
}