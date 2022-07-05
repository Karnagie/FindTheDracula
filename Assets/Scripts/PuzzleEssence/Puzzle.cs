using System;
using UnityEngine;

namespace PuzzleEssence
{
    public abstract class Puzzle : MonoBehaviour
    {
        public Action OnSolved;

        protected void Solve()
        {
            OnSolved?.Invoke();
        }
    }
}