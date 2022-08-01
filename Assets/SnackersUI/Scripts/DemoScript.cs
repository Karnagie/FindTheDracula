using System.Collections;
using UnityEngine;

namespace SnackersUI
{
    public class DemoScript : MonoBehaviour
    {
        [SerializeField] private TweenAnimatedUIElement[] _elements;
        [SerializeField] private TrophyRoad _trophyRoad;

        private void Awake()
        {
            _trophyRoad.SetCurrentLevel(2);
        }

        private void OnEnable()
        {
            StartCoroutine(Co_PlayDemo());
        }

        private IEnumerator Co_PlayDemo()
        {
            var i = 0;
            while (true)
            {
                yield return new WaitForSeconds(1f);
                var element = _elements[i];
                element.Show();
                yield return new WaitForSeconds(1.5f);
                element.Hide();
                i = (i + 1) % _elements.Length;
            }
        }
    }
}