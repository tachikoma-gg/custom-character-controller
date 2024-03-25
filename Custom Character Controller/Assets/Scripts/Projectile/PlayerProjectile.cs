using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectilePoint;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float sprayAngle;

    private GameObject playerCamera;
    private int currentAmmo;

    void Start()
    {
        playerCamera = FindObjectOfType<MoveCamera>().gameObject;
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            Instantiate(projectilePrefab, projectilePoint.position, Spray() * transform.rotation);
            currentAmmo--;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
    }

    Quaternion Spray()
    {
        float x = Random.Range(-sprayAngle, sprayAngle);
        float y = Random.Range(-sprayAngle, sprayAngle);

        Quaternion sprayDirection = Quaternion.Euler(x, y, 0);

        return sprayDirection;
    }

    Quaternion AimDirection()
    {
        Ray ray = new Ray(playerCamera.transform.position, Vector3.forward);
        Physics.Raycast(ray, out RaycastHit hitData);

        Vector3 direction = (hitData.point - transform.position).normalized;

        return Quaternion.Euler(direction);
    }
}
