using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetHeight : MonoBehaviour
{
    [SerializeField] private float height;
    [SerializeField] private float smoothness;
    [SerializeField] private GameObject player;

    void LateUpdate()
    {
        CameraHeight();
    }

    void CameraHeight()
    {
        float cameraHeight = Mathf.Lerp(transform.position.y, player.transform.position.y + height, smoothness * Time.deltaTime);
        transform.position = new Vector3(player.transform.position.x, cameraHeight, player.transform.position.z);
    }
}
