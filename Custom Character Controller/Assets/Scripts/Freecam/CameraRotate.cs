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

        float angleBetween = Vector3.Angle(transform.position, cameraTarget.transform.position) - 90;

        float predictedAngle = angleBetween + input * rotationSpeed * Time.deltaTime;

        Debug.Log(angleBetween + ", " + predictedAngle);

        if(Mathf.Abs(predictedAngle) <= rotationX_Max)
        {
            transform.RotateAround(cameraTarget.transform.position, axis, input * rotationSpeed * Time.deltaTime);
        }

        transform.LookAt(cameraTarget.transform.position);
    }

    void MoveCamera()
    {
        float x = transform.position.x - player.transform.position.x;
        float z = transform.position.z - player.transform.position.z;
        float height = transform.position.y - player.transform.position.y;

        if(height >= distanceDefault)
        {
            Vector3 direction = transform.position - player.transform.position;
            Vector3 positionTarget = player.transform.position + direction.normalized * distanceDefault;

            transform.position = Vector3.Lerp(transform.position, positionTarget, smoothness * Time.deltaTime);
        }
        else
        {
            Vector3 direction = new Vector3(x, 0, z).normalized;
        
            float a = Mathf.Asin(height / distanceDefault);
            float c = (Mathf.PI / 2) - a;

            float distance = Mathf.Sqrt(Mathf.Pow(height, 2) + Mathf.Pow(distanceDefault, 2) - 2 * height * distanceDefault * Mathf.Cos(c));

            Vector3 positionTarget = player.transform.position + direction * distance + new Vector3(0, height, 0);

            transform.position = Vector3.Lerp(transform.position, positionTarget, smoothness * Time.deltaTime);
        }
    }
}
