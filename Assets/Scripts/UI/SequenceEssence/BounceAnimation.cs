using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SequenceEssence
{
    public class BounceAnimation : Animation
    {
        public override void Init()
        {
            var image = GetComponent<Image>();
            if(image)image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            
            var text = GetComponent<TMP_Text>();
            if(text)text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }

        public override void Animate()
        {
            transform.DORewind ();
            transform.DOPunchScale (new Vector3 (1, 1, 1), .1f);
            GetComponent<Image>()?.DOFade(1, 0.1f);
            GetComponent<TMP_Text>()?.DOFade(1, 0.1f);
        }
    }
}