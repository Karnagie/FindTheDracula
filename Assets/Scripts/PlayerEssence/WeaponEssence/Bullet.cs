using DG.Tweening;
using UnityEngine;
using VampireEssence;

namespace PlayerEssence.WeaponEssence
{
    public class Bullet : MonoBehaviour
    {
        public void MoveTo(Vampire vampire)
        {
            transform.DOMove(vampire.Heart, 0.2f).OnComplete((() =>
            {
                vampire.Die();
                Destroy(gameObject);
            }));
        }
    }
}