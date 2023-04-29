using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public EWeaponType ActualWeaponType;

    private void Awake()
    {
        Instance = this;
    }
}
