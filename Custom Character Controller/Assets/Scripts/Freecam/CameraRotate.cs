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

    [SerializeField] private GameObject marker;

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

        // Clamp rotation to prevent jitter at x = 90 or x = -90.

        transform.LookAt(cameraTarget.transform.position);
    }

    void MoveCamera()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = transform.position - player.transform.position;

        Ray ray = new Ray(player.transform.position, direction);
        Physics.Raycast(ray, out RaycastHit hitData, distanceDefault);

        float hitDistance = Vector3.Distance(hitData.point, player.transform.position);

        marker.transform.position = hitData.point;

        Debug.Log(direction);

        if(distance > distanceDefault)
        {
            float distanceTarget = (hitData.point.y != 0 && hitDistance < distanceDefault) ? hitDistance : distanceDefault;

            Vector3 targetPosition = player.transform.position + direction.normalized * distanceTarget;

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        }
    }
}
