using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifespan;

    public float _speed
    {
        get
        {
            return speed;
        }
    }

    private Rigidbody projectileRb;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
