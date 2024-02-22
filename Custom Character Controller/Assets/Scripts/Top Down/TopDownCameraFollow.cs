using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothness;
    [SerializeField] private Vector3 cameraOffset;

    void Update()
    {
        Vector3 targetPosition = player.transform.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
    }

    void LateUpdate()
    {
        // transform.LookAt(player.transform.position);
    }
}
