using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public delegate void FireWeapon();
    public event FireWeapon FireWeaponEvent; 

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed Left Mouse");
            FireWeaponEvent.Invoke();
        }
    }
}
