using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothness;
    [SerializeField] private Vector3 cameraOffset;

    [SerializeField] private float offsetFactor;

    void LateUpdate()
    {
        float x = Input.GetAxis("Horizontal_2");
        float z = -Input.GetAxis("Vertical_2");

        Vector3 offsetAdjustment = new Vector3(x, 0, z) * offsetFactor;
        
        Vector3 targetPosition = player.transform.position + cameraOffset + offsetAdjustment;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);

        // transform.position = player.transform.position + cameraOffset;
    }
}
