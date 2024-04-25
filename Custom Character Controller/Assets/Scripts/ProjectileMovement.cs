using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float projectileLifetimeSeconds;
    [SerializeField] private float projectileSpeed;

    void Start() => Destroy(gameObject, projectileLifetimeSeconds);
    void Update() => transform.Translate(projectileSpeed * Time.deltaTime * Vector3.forward);
    void OnCollisionEnter(Collision collision) => Destroy(gameObject);
}
