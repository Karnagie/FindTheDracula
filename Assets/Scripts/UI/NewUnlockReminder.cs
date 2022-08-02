using System;
using System.Collections;
using Core.SaveAndLoadEssence;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace UI
{
    public class NewUnlockReminder : MonoBehaviour
    {
        [SerializeField] private bool _tool;
        
        [Inject] private ISaveAndLoadSystem _saveAndLoad;
        
        private void Awake()
        {
            transform.DOShakeScale(0.5f, Vector3.one / 2);
            StartCoroutine(Waiting());
            if (_saveAndLoad.OpenedNewTool == false && _tool)
            {
                gameObject.SetActive(false);
            }else if (_saveAndLoad.OpenedNewWeapon == false && !_tool)
            {
                gameObject.SetActive(false);
            }
        }

        private IEnumerator Waiting()
        {
            yield return new WaitForSeconds(1);
            transform.DOShakeScale(0.5f,  Vector3.one/2).OnComplete((() => { StartCoroutine(Waiting());}));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            if(_tool)_saveAndLoad.ResetOpenedNewTool();
            else _saveAndLoad.ResetOpenedWeapon();
        }
    }
}