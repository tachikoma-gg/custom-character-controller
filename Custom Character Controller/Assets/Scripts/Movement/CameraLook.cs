using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform cameraTargetTransform;

    void LateUpdate()
    {
        transform.LookAt(cameraTargetTransform.position);
    }
}
