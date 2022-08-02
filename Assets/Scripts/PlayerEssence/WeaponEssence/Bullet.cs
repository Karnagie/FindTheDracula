using AliveEssence;
using DG.Tweening;
using UnityEngine;

namespace PlayerEssence.WeaponEssence
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _number;
        [SerializeField] private bool _rotate = false;
        public void MoveTo(AliveObject aliveObject)
        {
            transform.DOMove(aliveObject.Heart, 0.5f).OnComplete((() =>
            {
                aliveObject.Die(_number, true);
                Destroy(gameObject);
            }));
            if (_rotate)
            {
                transform.DORotate(new Vector3(360, 0, 0), 0.5f, RotateMode.LocalAxisAdd);
            }
        }
    }
}