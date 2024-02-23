using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cameraTarget;

    [SerializeField] private float targetDistance;
    [SerializeField] private float smoothness;
    [SerializeField] private float cameraHeight;

    void Update()
    {
        ResetCamera();
    }

    void LateUpdate()
    {
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;

        float x = transform.position.x - playerX;
        float y = CameraHeight();
        float z = transform.position.z - playerZ;

        Vector3 direction = new Vector3(x, 0, z);
        Vector3 playerPosition = new Vector3(playerX, 0, playerZ);
        Vector3 targetPosition = direction.normalized * targetDistance + playerPosition + new Vector3(0, y, 0);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        transform.LookAt(cameraTarget.transform.position);
    }

    float CameraHeight()
    {
        Vector3 ground = GroundDetect();
        Vector3 ceiling = CeilingDetect(ground);

        float groundHeight = (ground.y + 1 < player.transform.position.y) ? ground.y : player.transform.position.y; 

        float y = (ceiling.y == 0 && ceiling.y < (ground.y + cameraHeight)) ? (groundHeight + cameraHeight) : ceiling.y;

        return y;
    }

    Vector3 GroundDetect()
    {
        float x = transform.eulerAngles.x;
        Vector3 origin = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        Ray ray = new Ray(origin, Quaternion.Euler(x, 0, 0) * Vector3.down);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);

        return hitData.point;
    }

    Vector3 CeilingDetect(Vector3 ground)
    {
        float x = transform.eulerAngles.x;
        Vector3 origin = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        Ray ray = new Ray(origin, Quaternion.Euler(x, 0, 0) * Vector3.up);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData, 5);

        return hitData.point;
    }

    void ResetCamera()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // rotate around player to get behind player.
        }
    }
}
