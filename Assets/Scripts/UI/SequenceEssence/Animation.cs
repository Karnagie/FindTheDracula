using UnityEngine;

namespace UI.SequenceEssence
{
    public abstract class Animation : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Animate();
    }
}