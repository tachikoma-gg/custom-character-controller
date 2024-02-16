using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float targetDistance;
    [SerializeField] private float smoothness;
    [SerializeField] private float cameraHeight;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void LateUpdate()
    {
        float x = transform.position.x - player.transform.position.x;
        float y = player.transform.position.y + cameraHeight;
        float z = transform.position.z - player.transform.position.z;

        Vector3 direction = new Vector3(x, y, z);
        Vector3 targetPosition = direction.normalized * targetDistance + player.transform.position;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
    }
}
