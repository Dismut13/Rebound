using Assets.Scripts.Projectiles;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform arrow;
        [SerializeField] private Projectile projectile;
        [SerializeField] private float attackForce = 2;

        public bool HasAttacked { get; private set; }

        public void Attack()
        {
            if (!HasAttacked)
            {
                HasAttacked = true;

                var force = new Vector3(Mathf.Cos(arrow.eulerAngles.z * Mathf.Deg2Rad), -Mathf.Sin(arrow.eulerAngles.z * Mathf.Deg2Rad)) * attackForce;
                projectile.InstantiatePrefab(arrow.position, force);
            }
        }
    }
}