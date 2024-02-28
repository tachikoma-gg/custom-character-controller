using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cameraTarget;

    [SerializeField] private float distanceDefault;
    [SerializeField] private float smoothness;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float rotationX_Max;
    [SerializeField] private float rotationX_Min;
    [SerializeField] private Vector3 rotationDefault;

    [SerializeField] private LayerMask ignoreRaycast;

    void Update()
    {
        MoveCamera();
    }

    void LateUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float x = Input.GetAxis("Vertical_2");
        float y = Input.GetAxis("Horizontal_2");

        float input = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        Vector3 axis = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(-x, -y, 0);

        transform.RotateAround(player.transform.position, axis, input * rotationSpeed * Time.deltaTime);
        transform.LookAt(cameraTarget.transform.position);
    }

    void MoveCamera()
    {
        float x = transform.position.x - player.transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z - player.transform.position.z;

        Vector3 direction = new Vector3(x, y, z).normalized;
        Vector3 targetPosition = direction * distanceDefault + player.transform.position;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
    }
}
