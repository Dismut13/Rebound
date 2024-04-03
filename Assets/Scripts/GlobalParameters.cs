using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GlobalParameters : MonoBehaviour
    {
        public static GlobalParameters Instance;

        [field: SerializeField]
        public Transform ProjectileHolder;

        private void Awake()
        {
            Instance = this;
        }
    }
}