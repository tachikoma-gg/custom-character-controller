using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float targetDistance;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private float height;

    private float smoothness = 10;

    void LateUpdate()
    {
        // OrbitCamera();
        FollowPlayer();
    }

    void OrbitCamera()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        float input = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        Vector3 axis = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(-y, x, 0);

        transform.position = player.transform.position + Quaternion.Euler(0, transform.eulerAngles.y, 0) * offset;
        
        transform.RotateAround(player.transform.position, axis, smoothness * input);
        transform.LookAt(player.transform.position);
    }

    void FollowPlayer()
    {
        float x = transform.position.x - player.transform.position.x;
        float z = transform.position.z - player.transform.position.z;

        Vector3 direction = new Vector3(x, 0, z);
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 targetPosition = direction.normalized * targetDistance + playerPosition + new Vector3(0, height, 0);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);

        transform.LookAt(player.transform.position + targetOffset);
    }
}
