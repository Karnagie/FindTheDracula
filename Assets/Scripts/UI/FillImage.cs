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

        private Tool _tool;
        private bool _isWorking;


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
                if(_isWorking) return;
                _isWorking = true;
                transform.DOKill(true);
                _percents.gameObject.SetActive(true);
                if (_tool != null)
                {
                    //_tool.gameObject.SetActive(false);
                    _tool = null;
                }
                Debug.Log($"Start animating {_inventory.IsAllFilled()} {_saveAndLoad.GetCurrentEquipmentOpening()}");
                int w = _saveAndLoad.IsNextWeaponOrQuestion();
                if ((w == 0 && _saveAndLoad.GetCurrentWeaponOpening() != 0 && _inventory.IsAllFilled() == false)
                    || _equipmentInventory.IsAllFilled())
                {
                    Debug.Log( _equipmentInventory.IsAllFilled());
                    _inventory.AddPercents(_fillAmount);
                    var count = _saveAndLoad.FillWeaponPercent();
                    //_image.fillAmount = 0;
                    //_weapons[_saveAndLoad.GetCurrentWeaponOpening()].Fill(0);
                    // switch (count)
                    // {
                    //     case 0: 
                    //         _image.fillAmount = 0;
                    //         break;
                    //     case -1:
                    //         _image.fillAmount = 0;
                    //         break;
                    // }
                
                    //transform.DOKill(true);
                    transform.localScale = Vector3.zero;
                    //0.013536f
                    //_image.DOFade(1, .25f);
                    float fullFill = _image.fillAmount + _fillAmount;
                    await Task.Delay(500);
                    
                    transform.DOScale (new Vector3 (1,1,1), .25f);
                        _weapons[_saveAndLoad.GetCurrentWeaponOpening()].gameObject.SetActive(true);
                        while (_image.fillAmount < _saveAndLoad.FillWeaponPercent() )
                        {
                            _image.fillAmount += _fillSpeed * Time.deltaTime;
                            string per = (_image.fillAmount*100).ToString();
                            int per1 = (int) (_image.fillAmount * 100);
                            // if (per.Length > 2)
                            // {
                            //     per = per.Remove(1, per.Length-2);
                            // }
                            _percents.text = per1+"%";
                            _weapons[_saveAndLoad.GetCurrentWeaponOpening()].Fill(_image.fillAmount);
                            await Task.Yield();
                        }
                        _image.fillAmount = _saveAndLoad.FillWeaponPercent();
                        _weapons[_saveAndLoad.GetCurrentWeaponOpening()].Fill(_image.fillAmount);
                        await Task.Delay(1000);
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
                            _tool = tool;
                            foreach (var system in _onGetting)
                            {
                                system.Play();
                            }
                            _weapons[_saveAndLoad.GetCurrentWeaponOpening()].gameObject.SetActive(false);
                            transform.DOScale (new Vector3 (0,0,0), .25f).OnComplete((() =>  
                                tool.gameObject.SetActive(false))).SetDelay(5);
                            await Task.Delay(5000);
                            _image.fillAmount = 0;
                            _percents.text = "";
                           // transform.DOScale (new Vector3 (0,0,0), .25f).OnComplete((() =>  
                           //      tool.gameObject.SetActive(false)));
                        }
                        else
                        {
                            transform.DOScale (new Vector3 (0,0,0), .25f).OnComplete((() =>  
                                _weapons[_saveAndLoad.GetCurrentWeaponOpening()].gameObject.SetActive(false)));
                        }
                    
                }
                else if (_saveAndLoad.GetCurrentEquipmentOpening() != 0 || _inventory.IsAllFilled())
                {
                    Debug.Log(_saveAndLoad.FillEquipmentPercent());
                    _equipmentInventory.AddPercents(_fillAmount);
                    var count = _saveAndLoad.FillEquipmentPercent();
                    // _image.fillAmount = 0;
                    // switch (count)
                    // {
                    //     case 0: 
                    //         _image.fillAmount = 0;
                    //         break;
                    //     case -1:
                    //         _image.fillAmount = 0;
                    //         break;
                    // }
                    //transform.DOKill(true);
                    transform.localScale = Vector3.zero;
                    //_image.DOFade(1, .25f);
                    float fullFill = _image.fillAmount + _fillAmount;
                    await Task.Delay(500);
                    
                    transform.DOScale (new Vector3 (1,1,1), .25f);
                        _tools[_saveAndLoad.GetCurrentEquipmentOpening()].gameObject.SetActive(true);
                        //_tools[_saveAndLoad.GetCurrentEquipmentOpening()].Fill(0);
                    
                        while (_image.fillAmount < _saveAndLoad.FillEquipmentPercent() )
                        {
                            _image.fillAmount += _fillSpeed * Time.deltaTime;
                            string per = (_image.fillAmount*100).ToString();
                            int per1 = (int) (_image.fillAmount * 100);
                            // if (per.Length > 2)
                            // {
                            //     per = per.Remove(1, per.Length-2);
                            // }
                            _percents.text = per1+"%";
                            _tools[_saveAndLoad.GetCurrentEquipmentOpening()].Fill(_image.fillAmount);
                            await Task.Yield();
                        }
                        _image.fillAmount = _saveAndLoad.FillEquipmentPercent();
                        _tools[_saveAndLoad.GetCurrentEquipmentOpening()].Fill(_image.fillAmount);
                        await Task.Delay(1000);
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
                            _tool = tool;
                            foreach (var system in _onGetting)
                            {
                                system.Play();
                            }
                            _tools[_saveAndLoad.GetCurrentEquipmentOpening()].gameObject.SetActive(false);
                            transform.DOScale (new Vector3 (0,0,0), .25f).OnComplete((() =>
                            {
                                tool.gameObject.SetActive(false);
                            })).SetDelay(5);;
                            await Task.Delay(5000);
                            _image.fillAmount = 0;
                            _percents.text = "";
                        }
                        else
                        {
                           transform.DOScale (new Vector3 (0,0,0), .25f).OnComplete((() =>
                            {
                                _tools[_saveAndLoad.GetCurrentEquipmentOpening()].gameObject.SetActive(false);
                            }));
                        }
                }

                _isWorking = false;
                //_percents.gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                // ignored
            }
        }
    }
}