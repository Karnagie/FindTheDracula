using System.Threading.Tasks;
using UnityEngine;

namespace UI.SequenceEssence
{
    public class AnimationSequence : MonoBehaviour
    {
        [SerializeField] private Animation[] _animations;
        [SerializeField] private float _delay = 0.5f;

        public async void Play()
        {
            foreach (var animation in _animations)
            {
                animation.Init();
            }
            
            foreach (var animation in _animations)
            {
                await Task.Delay((int) (_delay * 1000));
                animation.Animate();
                await Task.Delay((int) (_delay * 1000));
            }
        }
    }
}