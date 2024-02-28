using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cameraTarget;

    [SerializeField] private float targetDistance;
    [SerializeField] private float smoothness;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float cameraHeightDefault;
    [SerializeField] private float rayLength;
    [SerializeField] private float cameraHeightMax;
    [SerializeField] private float cameraHeightMin;

    private float cameraHeight;

    void Start()
    {
        cameraHeight = cameraHeightDefault;
    }

    void Update()
    {
        ResetCamera();
        RotateCamera();
    }

    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        float x = transform.position.x - player.transform.position.x;
        float y = CameraHeight() - player.transform.position.y;
        float z = transform.position.z - player.transform.position.z;

        Vector3 direction = new Vector3(x, 0, z);
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 targetPosition = direction.normalized * targetDistance + playerPosition + new Vector3(0, y, 0);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        transform.LookAt(cameraTarget.transform.position);
    }

    void RotateCamera()
    {
        float inputX = Input.GetAxis("Horizontal_2");
        float rotate = rotationSpeed * inputX * Time.deltaTime;

        transform.RotateAround(cameraTarget.transform.position, Vector3.up, rotate);
    }

    float CameraHeight()
    {
        // Need to redo this.

        float inputY = Input.GetAxis("Vertical_2");
        cameraHeight -= inputY * Time.deltaTime * 15;

        Vector3 ground = GroundDetect();
        Vector3 ceiling = CeilingDetect();

        float heightMax = (ceiling.y < player.transform.position.y + cameraHeightMax && ceiling.y != 0) ? ceiling.y : (player.transform.position.y + cameraHeightMax);
        float heightMin = (ground.y > player.transform.position.y + cameraHeightMin) ? ground.y : (player.transform.position.y + cameraHeightMin);

        cameraHeight = Mathf.Clamp(cameraHeight, player.transform.position.y + heightMin, player.transform.position.y + heightMax);

        float y = cameraHeight;

        return y;
    }

    Vector3 GroundDetect()
    {
        float x = transform.eulerAngles.x;
        Vector3 origin = new(transform.position.x, player.transform.position.y, transform.position.z);
        Ray ray = new(origin, Quaternion.Euler(x, 0, 0) * Vector3.down);
        Physics.Raycast(ray, out RaycastHit hitData);

        return hitData.point;
    }

    Vector3 CeilingDetect()
    {
        float x = transform.eulerAngles.x;
        Vector3 origin = new(transform.position.x, player.transform.position.y, transform.position.z);

        Ray ray = new(origin, Quaternion.Euler(x, 0, 0) * Vector3.up);
        Physics.Raycast(ray, out RaycastHit hitData, rayLength);

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
