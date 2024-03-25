using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthMax;

    private int healthCurrent;

    void Start()
    {
        healthCurrent = healthMax;
    }

    public void TakeDamage(int damage)
    {
        healthCurrent -= damage;

        if(healthCurrent <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
