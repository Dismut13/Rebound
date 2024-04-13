using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        public int EnemyCount { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void OnEnemyCreate()
        {
            EnemyCount++;
        }

        public void OnEnemyDie()
        {
            EnemyCount--;
            if (EnemyCount == 0)
                GameManager.Instance.Win();
        }
    }
}