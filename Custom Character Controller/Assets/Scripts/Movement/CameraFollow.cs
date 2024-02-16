using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Transform cameraTargetTransform;

    [SerializeField] private float targetDistance;
    [SerializeField] private float smoothness;
    [SerializeField] private float cameraHeight;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        cameraTargetTransform = GameObject.Find("Camera Target").transform;
    }

    void LateUpdate()
    {
        float x = transform.position.x - player.transform.position.x;
        float y = player.transform.position.y + cameraHeight;
        float z = transform.position.z - player.transform.position.z;

        // Find direction from player towards camera.
        Vector3 direction = new Vector3(x, y, z);
        // Move camera to targetDistance away from player in that direction.
        Vector3 targetPosition = direction.normalized * targetDistance + player.transform.position;

        // Smoothly move camera towards targetPosition.
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);

        // Point camera towards cameraTarget, which is a point above the player.
        transform.LookAt(cameraTargetTransform.position);
    }
}
