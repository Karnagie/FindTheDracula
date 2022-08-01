using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnackersUI
{
    public class TrophyRoad : MonoBehaviour
    {
        [SerializeField] private Image _trophyRoadDotPrefab;
        [SerializeField] private Image _finalTrophyRoadDotPrefab;
        [SerializeField] private Transform _dotsContainer;
        [SerializeField, Min(3)] private int _numLevelsToShow = 5;
        [SerializeField] private Color _completeDotColor = Color.white;
        [SerializeField] private Color _futureDotColor = new Color(1, 1, 1, 0.5f);


        private List<Image> _dots = new List<Image>();

        private void Awake()
        {
            for (var i = 0; i < _numLevelsToShow; i++)
            {
                var prefab = i == _numLevelsToShow - 1 && _finalTrophyRoadDotPrefab != null
                    ? _finalTrophyRoadDotPrefab
                    : _trophyRoadDotPrefab;
                var dot = Instantiate(prefab, _dotsContainer);
                _dots.Add(dot);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">Zero-based index of level</param>
        public void SetCurrentLevel(int level)
        {
            var remainder = level % _dots.Count;
            for (var i = 0; i < _dots.Count; i++)
            {
                _dots[i].color = i <= remainder ? _completeDotColor : _futureDotColor;
            }
        }
    }
}