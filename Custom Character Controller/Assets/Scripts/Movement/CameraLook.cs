using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private Transform cameraTargetTransform;

    void Start()
    {
        cameraTargetTransform = GameObject.Find("Camera Target").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(cameraTargetTransform.position);
    }
}
