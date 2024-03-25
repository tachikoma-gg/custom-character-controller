using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    
    [SerializeField] private GameObject impact;
    [SerializeField] private LayerMask enemies;
    
    private Vector3 surfaceLastPoint;

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        Physics.Raycast(ray, out RaycastHit hitData);

        if(hitData.point != Vector3.zero)
        {
            surfaceLastPoint = hitData.point;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject impactProjectile = Instantiate(impact, surfaceLastPoint , transform.rotation);

        impactProjectile.transform.SetParent(collision.gameObject.transform);
    }
}
