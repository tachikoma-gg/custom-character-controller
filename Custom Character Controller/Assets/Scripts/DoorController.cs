using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject hinge;

    private bool isOpen;
    private bool opening;

    private GameObject player;

    void Start()
    {
        player = FindObjectOfType<MovePlayer>().gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && InRange() && !opening)
        {
            StartCoroutine(ActuateDoor());
            opening = true;
        }
        
    }

    IEnumerator ActuateDoor()
    {
        float timeElapsed = 0;
        float duration = 0.5f;
        float target = 90;

        if(isOpen)
        {
            target = 0;
        }

        while (timeElapsed < duration)
        {
            float rotation = hinge.transform.eulerAngles.y;
            float current = Mathf.LerpAngle(rotation, target, timeElapsed / duration);
            hinge.transform.rotation = Quaternion.Euler(0, current, 0);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        isOpen = !isOpen;
        opening = false;
    }

    bool InRange()
    {
        bool inRange = false;

        if(Vector3.Distance(transform.position, player.transform.position) <= 3)
        {
            inRange = true;
        }

        return inRange;
    }
}
