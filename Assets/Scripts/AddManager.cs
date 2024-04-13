using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class AddManager : MonoBehaviour
    {
        [field: SerializeField]
        public float AddCooldown = 5;

        public static AddManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(transform);
                StartCoroutine(ShowAddAfterTime());
            }
        }

        private IEnumerator ShowAddAfterTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(AddCooldown);
                Debug.Log("ShowAdd");
            }
        }
    }
}
