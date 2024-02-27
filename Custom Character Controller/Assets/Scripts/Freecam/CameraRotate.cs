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

    void LateUpdate()
    {
        RotateCamera();
        MoveCamera();
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
        float distance = Vector3.Distance(transform.position, cameraTarget.transform.position);
        Vector3 direction = transform.position - cameraTarget.transform.position;

        if(distance != distanceDefault)
        {
            Vector3 targetPosition = cameraTarget.transform.position + direction.normalized * distanceDefault;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        }
    }
}
