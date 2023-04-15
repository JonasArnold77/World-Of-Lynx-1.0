using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void Start()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
    }

    public void SetColliderActive()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = true;
    }

    public void SetColliderInactive()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
    }
}
