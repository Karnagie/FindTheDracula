using System;
using UnityEngine;

namespace UI
{
    public class NewToolInInventory : MonoBehaviour
    {
        [SerializeField] private string _id;
        private void Awake()
        {
            if (PlayerPrefs.GetInt(_id, 0) == 1)
            {
                gameObject.SetActive(false);
            }
        }

        public void Clicked()
        {
            PlayerPrefs.SetInt(_id, 1);
            gameObject.SetActive(false);
        }
    }
}