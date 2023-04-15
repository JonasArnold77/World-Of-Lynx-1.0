using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject ActualWeapon;

    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
