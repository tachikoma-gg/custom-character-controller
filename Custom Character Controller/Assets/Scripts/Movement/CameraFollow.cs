using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float distanceMin, distanceMax;
    [SerializeField] private float smoothness;

    private float targetSpeed;
    private float currentSpeed;

    void LateUpdate()
    {
        MoveAnchor();
    }

    void MoveAnchor()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance > distanceMax)
        {
            targetSpeed = player.GetComponent<PlayerController>().speed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothness * Time.deltaTime);
        }
        else if(distance < distanceMin)
        {
            targetSpeed = -1 * player.GetComponent<PlayerController>().speed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothness * Time.deltaTime);
        }
        else
        {
            targetSpeed = 0;
            currentSpeed = 0;
        }

        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    void GroundDetect()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
    }
}
