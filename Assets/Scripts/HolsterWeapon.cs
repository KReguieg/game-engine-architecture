using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterWeapon : MonoBehaviour {
    public GameObject Holster;
    public GameObject[] WeaponPrefabs;

    public void HolsterWeaponNumber(int number)
    {
        if(Holster.transform.childCount != 0)
            Destroy(Holster.transform.GetChild(0).gameObject);
        GameObject equipedWeapon = Instantiate(WeaponPrefabs[number], Holster.transform);
        equipedWeapon.transform.localRotation = Quaternion.identity;
        equipedWeapon.transform.localPosition = Vector3.zero;
    }
}
