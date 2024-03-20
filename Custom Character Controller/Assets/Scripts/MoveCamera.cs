using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float cameraDistanceInitial;
    [SerializeField] private float cameraDistanceMax;
    [SerializeField] private float cameraAngleInitial;

    private float cameraDistance;
    private Vector3 cameraAngle;

    [SerializeField] private GameObject cameraTarget;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float scrollSpeed;

    private Vector3 mouseInput;

    private bool _lockState;
    public bool lockState
    {
        get
        {
            return _lockState;
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cameraDistance = cameraDistanceInitial;
        cameraAngle.x = cameraAngleInitial;
    }

    void LateUpdate()
    {
        mouseInput.x = Input.GetAxis("Mouse X");
        mouseInput.y = Input.GetAxis("Mouse Y");
        mouseInput.z = Input.mouseScrollDelta.y;

        // Set camera angles
        cameraAngle.y += rotateSpeed * Time.deltaTime * mouseInput.x;
        cameraAngle.x -= rotateSpeed * Time.deltaTime * mouseInput.y;
        cameraAngle.x = Mathf.Clamp(cameraAngle.x, -89, 89);

        // Calculate camera distance
        cameraDistance += mouseInput.z * scrollSpeed * Time.deltaTime;
        cameraDistance = Mathf.Clamp(cameraDistance, 0, cameraDistanceMax);

        // Translate & Rotate camera
        cameraTarget.transform.rotation = Quaternion.Euler(cameraAngle.x, cameraAngle.y, 0);
        transform.position = cameraTarget.transform.position + cameraTarget.transform.forward * -cameraDistance;

        // Determine lock state
        _lockState = (Input.GetKey(KeyCode.LeftShift) || cameraDistance == 0) ? true : false;

        if(lockState)
        {
            // Locked camera
            transform.rotation = Quaternion.Euler(cameraAngle.x, cameraAngle.y, 0);
            player.transform.rotation = Quaternion.Euler(0, cameraAngle.y, 0);
        }
        else
        {
            // Free camera
            transform.LookAt(cameraTarget.transform.position);
        }
    }
}
