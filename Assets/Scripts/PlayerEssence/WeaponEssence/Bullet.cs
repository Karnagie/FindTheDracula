using AliveEssence;
using DG.Tweening;
using UnityEngine;

namespace PlayerEssence.WeaponEssence
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _number;
        public void MoveTo(AliveObject aliveObject)
        {
            transform.DOMove(aliveObject.Heart, 0.2f).OnComplete((() =>
            {
                aliveObject.Die(_number, true);
                Destroy(gameObject);
            }));
        }
    }
}