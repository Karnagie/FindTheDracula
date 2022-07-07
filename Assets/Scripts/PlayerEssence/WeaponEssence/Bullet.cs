using AliveEssence;
using DG.Tweening;
using UnityEngine;

namespace PlayerEssence.WeaponEssence
{
    public class Bullet : MonoBehaviour
    {
        public void MoveTo(AliveObject aliveObject)
        {
            transform.DOMove(aliveObject.Heart, 0.2f).OnComplete((() =>
            {
                aliveObject.Die();
                Destroy(gameObject);
            }));
        }
    }
}