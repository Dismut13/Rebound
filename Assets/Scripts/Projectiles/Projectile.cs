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
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag(TagManager.Player))
            {
                GameManager.Instance.Defeat();
                Destroy(gameObject);
            }

            if (collisionCount == 5)
                Destroy(gameObject);
        }

        public void InstantiatePrefab(Vector3 position, Vector3 force)
        {
            var projectile = Instantiate(gameObject, position, Quaternion.identity, GlobalParameters.Instance.ProjectileHolder).GetComponent<Projectile>();
            projectile.rigidbody.AddForce(force);
        }
    }
}