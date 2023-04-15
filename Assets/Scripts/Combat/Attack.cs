using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator _Animator;
    private bool IsInAttackingTimeWindow;
    private PlayerAnimation _PlayerAnimation;

    private void Start()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsInAttackingTimeWindow)
        {
            if (CheckForComboTimeWindow())
            {
                //_PlayerAnimation.PlayNextAttack
            }
        }
    }

    public void SetColliderActive()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = true;
        IsInAttackingTimeWindow = true;
    }

    public void SetColliderInactive()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
        IsInAttackingTimeWindow = false;
    }

    private bool CheckForComboTimeWindow()
    {
        if(GetCurrentAnimatorTime() >= GetDurationOfAnimatorClip() / 2 && )
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                return true;
            }
        }
        return false;
    }

    public float GetCurrentAnimatorTime(int layer = 2)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float currentTime = animState.normalizedTime % 1;
        return currentTime;
    }

    public float GetDurationOfAnimatorClip(int layer = 2)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float duration = animState.length % 1;
        return duration;
    }
}
