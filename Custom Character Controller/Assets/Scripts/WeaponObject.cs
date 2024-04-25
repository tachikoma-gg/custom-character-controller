using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float sprayAngle;
    [SerializeField] private Transform firingPoint;
    
    [SerializeField] private int ammoMax;
    private int ammoCurrent;

    [SerializeField] private float cooldownTimeSeconds;
    [SerializeField] private float reloadCooldownTimeSeconds;

    private bool ready = true;

    void Start()
    {
        ammoCurrent = ammoMax;
        FindObjectOfType<WeaponsController>().FireWeaponEvent += FireWeapon; // Comment out if you're only using Line 29.
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) Reload();
        // if(Input.GetMouseButtonDown(0)) FireWeapon();    // Use this if you only have one weapon. Simpler.
    }

    void FireWeapon()
    {
        // if(!ready || ammoCurrent <= 0)
        if(!ready)
        return;

        ready = false;
        StartCoroutine(nameof(CooldownTimer));

        // ammoCurrent--;

        for (int i = 0; i < numberOfProjectiles; i++) SpawnProjectile();
    }

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(cooldownTimeSeconds);
        ready = true;
    }

    IEnumerator ReloadCooldownTimer()
    {
        yield return new WaitForSeconds(reloadCooldownTimeSeconds);
        ready = true;
    }

    void SpawnProjectile()
    {
        // redo spray pattern to make it circular.

        float x = transform.eulerAngles.x + Random.Range(-sprayAngle, sprayAngle);
        float y = transform.eulerAngles.y + Random.Range(-sprayAngle, sprayAngle);
        float z = transform.eulerAngles.z + Random.Range(-sprayAngle, sprayAngle);

        Vector3 randomSprayAngle = new(x, y, z);

        Instantiate(projectilePrefab, firingPoint.position, Quaternion.Euler(randomSprayAngle));
    }

    void Reload()
    {
        ammoCurrent = ammoMax;

        ready = false;
        StartCoroutine(nameof(ReloadCooldownTimer));
    }
}
