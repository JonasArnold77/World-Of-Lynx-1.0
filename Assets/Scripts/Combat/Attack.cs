using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator _Animator;
    private bool IsInAttackingTimeWindow;
    private bool IsInComboTimeWindow;

    private bool CheckForComboTimeWindowCorutineIsRunning;

    private void Start()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(CheckForComboTimeWindow());

            PlayerAnimation.Instance.PlayNextAttack();

            if (IsInComboTimeWindow)
            {
                ComboManager.Instance.IncrementComboCounter();
            }
            else
            {
                ComboManager.Instance.ResetComboCounter();
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

    private IEnumerator CheckForComboTimeWindow()
    {
        if (!CheckForComboTimeWindowCorutineIsRunning)
        {
            CheckForComboTimeWindowCorutineIsRunning = true;

            yield return new WaitUntil(() => GetCurrentAnimatorTime() >= GetDurationOfAnimatorClip() / 2);
            IsInComboTimeWindow = true;
            yield return new WaitUntil(() => GetCurrentAnimatorTime() == GetDurationOfAnimatorClip());

            yield return new WaitForSeconds(0.5f);
            IsInComboTimeWindow = false;
            ComboManager.Instance.ResetComboCounter();

            CheckForComboTimeWindowCorutineIsRunning = false;
        }
    }

    public float GetCurrentAnimatorTime(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float currentTime = animState.normalizedTime;
        return currentTime;
    }

    public float GetDurationOfAnimatorClip(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float duration = animState.length;
        return duration;
    }
}
