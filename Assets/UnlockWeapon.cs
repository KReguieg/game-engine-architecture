using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockWeapon : MonoBehaviour {
    public GameObject Weapon;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { UnlockWeaponUseable(); });
    }

    private void UnlockWeaponUseable()
    {
        Weapon.GetComponent<VRTK.VRTK_InteractableObject>().isUsable = true;
    }
}
