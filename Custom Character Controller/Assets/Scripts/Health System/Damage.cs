using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;
    [SerializeField] private int damage;

    void OnCollisionEnter(Collision collision)
    {
        int layer = (int) Mathf.Log(enemies.value, 2);

        if(collision.gameObject.layer == layer)
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
