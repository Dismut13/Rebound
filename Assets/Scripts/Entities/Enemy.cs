using Assets.Scripts.Sounds;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Enemy : MonoBehaviour
    {
        private void Start()
        {
            EnemyManager.Instance.OnEnemyCreate();
        }

        public void Die()
        {
            GameSoundManager.Instance.PlayEnemyDie();
            EnemyManager.Instance.OnEnemyDie();

            Destroy(gameObject);
        }
    }
}