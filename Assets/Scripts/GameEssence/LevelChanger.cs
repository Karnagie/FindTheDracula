using System;
using Core.SaveAndLoadEssence;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameEssence
{
    public class LevelChanger : MonoBehaviour
    {
        [Inject] private ISaveAndLoadSystem _saveAndLoad;

        public void NextLevel()
        {
            SceneManager.LoadScene(_saveAndLoad.NextLevel);
        }
        
        public void RestartCurrentLevel()
        {
            SceneManager.LoadScene(_saveAndLoad.CurrentLevel);
        }
    }
}