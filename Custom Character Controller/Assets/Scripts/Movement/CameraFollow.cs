using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private GameObject cameraTarget;

    [SerializeField] private float targetDistance;
    [SerializeField] private float smoothness;
    [SerializeField] private float cameraHeight;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        cameraTarget = FindObjectOfType<CameraTargetHeight>().gameObject;
    }

    void LateUpdate()
    {
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;

        float x = transform.position.x - playerX;
        float y = CameraHeight();
        float z = transform.position.z - playerZ;

        // Find direction from player towards camera.
        Vector3 direction = new Vector3(x, 0, z);
        // Define player position minus height;
        Vector3 playerPosition = new Vector3(playerX, 0, playerZ);
        // Move camera to targetDistance away from player in that direction.
        Vector3 targetPosition = direction.normalized * targetDistance + playerPosition + new Vector3(0, y, 0);

        // Smoothly move camera towards targetPosition.
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);

        // Point camera towards cameraTarget, which is a point above the player.
        transform.LookAt(cameraTarget.transform.position);
    }

    float CameraHeight()
    {
        float x = transform.eulerAngles.x;
        Ray ray = new Ray(transform.position, Quaternion.Euler(x, 0, 0) * Vector3.down);    // Need to calculate world down, not camera down.
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);

        float y = hitData.point.y + cameraHeight;

        return y;
    }
}
