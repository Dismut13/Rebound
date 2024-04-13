using Assets.Scripts.Entities;
using Assets.Scripts.Sounds;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D rigidbody;
        private int collisionCount;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collisionCount++;

            if (collision.gameObject.CompareTag(TagManager.Enemy))
            {
                collision.gameObject.GetComponent<Enemy>().Die();
            }
            else if (collision.gameObject.CompareTag(TagManager.Player))
            {
                GameManager.Instance.Defeat();
                Die();
            }

            if (collisionCount == 5)
                Die();

            GameSoundManager.Instance.PlayRebound();
        }

        void Update()
        {
            if (!GetScreenWorldRect().Contains(transform.position))
            {
                Die();
            }
        }

        private Rect GetScreenWorldRect()
        {
            var camera = Camera.main;
            Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
            return (new Rect(bottomLeft.x, bottomLeft.y, topRight.x * 2f, topRight.y * 2f));
        }


        public void InstantiatePrefab(Vector3 position, Vector3 force)
        {
            var projectile = Instantiate(gameObject, position, Quaternion.identity, GlobalParameters.Instance.ProjectileHolder).GetComponent<Projectile>();
            projectile.rigidbody.AddForce(force);
        }

        private void Die()
        {
            if (EnemyManager.Instance.EnemyCount > 0)
                GameManager.Instance.Defeat();
            Destroy(gameObject);
        }
    }
}