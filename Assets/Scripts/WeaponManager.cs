using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Swap();
    }

    void Swap()
    {
        int weaponIndex = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1)) weaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) weaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) weaponIndex = 2;

        if ((Input.GetKeyDown(KeyCode.Alpha1)) || (Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKeyDown(KeyCode.Alpha3)))
        {
            weapons[weaponIndex].SetActive(true); 
        }
    }
   
}