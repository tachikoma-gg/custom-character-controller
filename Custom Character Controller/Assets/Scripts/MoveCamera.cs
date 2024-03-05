using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float targetDistance;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float height;

    private float smoothness = 10;

    void LateUpdate()
    {
        float x = transform.position.x - player.transform.position.x;
        float z = transform.position.z - player.transform.position.z;

        Vector3 direction = new Vector3(x, 0, z);
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 targetPosition = direction.normalized * targetDistance + playerPosition + new Vector3(0, height, 0);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        transform.LookAt(player.transform.position + offset);
    }
}
