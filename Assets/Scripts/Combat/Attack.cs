using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator _Animator;
    private bool IsInAttackingTimeWindow;
    private bool IsInComboTimeWindow;
    private bool FirstHit;

    private Coroutine WaitForResettingCoroutineÍnstance;

    private bool CheckForComboTimeWindowCorutineIsRunning;
    private bool WaitForResettingCoroutineIsActive;

    private void Start()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
        _Animator = GetComponent<Animator>();


    }

    private void Update()
    {
        CalculateCombo();
    }

    public void CalculateCombo()
    {
        //if (CheckIfAnimatorIsPlayingAttack())
        //{
        //    StartCoroutine(CheckForComboTimeWindow());
        //}


        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.LightHit)))
        {
            ComboManager.Instance.ResetAllComboCountersInsteadOfSelected(EControls.LightHit);
            if (WaitForResettingCoroutineIsActive)
            {
                StopCoroutine(WaitForResettingCoroutineÍnstance);
            }
            PlayerAnimation.Instance.PlayNextAttack(EControls.LightHit);
            WaitForResettingCoroutineÍnstance = StartCoroutine(WaitForResettingCoroutine(EControls.LightHit));

            InputManager.Instance.CollectingInputsList.Add(EControls.LightHit);
        }

        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.HardHit)))
        {
            ComboManager.Instance.ResetAllComboCountersInsteadOfSelected(EControls.HardHit);
            if (WaitForResettingCoroutineIsActive)
            {
                StopCoroutine(WaitForResettingCoroutineÍnstance);
            }
            PlayerAnimation.Instance.PlayNextAttack(EControls.HardHit);
            WaitForResettingCoroutineÍnstance = StartCoroutine(WaitForResettingCoroutine(EControls.HardHit));

            InputManager.Instance.CollectingInputsList.Add(EControls.LightHit);
        }
    }

    private IEnumerator WaitForResettingCoroutine(EControls control)
    {
        WaitForResettingCoroutineIsActive = true;
        yield return new WaitForSeconds(2f);
        ComboManager.Instance.Combos.Where(c => c.InputType == control).FirstOrDefault().ResetCounter();
        InputManager.Instance.CollectingInputsList.Clear();
        WaitForResettingCoroutineIsActive = false;
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

            //if I hit before the combo countr should be resetted
            //yield return new WaitWhile

            yield return new WaitUntil(() => GetCurrentAnimatorTime() >= GetDurationOfAnimatorClip() / 2);
            IsInComboTimeWindow = true;
            yield return new WaitUntil(() => GetCurrentAnimatorTime()>=GetDurationOfAnimatorClip());

            yield return new WaitForSeconds(0.5f);
            IsInComboTimeWindow = false;
            ComboManager.Instance.ResetComboCounter();
            FirstHit = false;
            CheckForComboTimeWindowCorutineIsRunning = false;
        }
    }

    public bool CheckIfAnimatorIsPlayingAttack(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        if (animState.IsTag("Attack"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetCurrentAnimatorTime(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float currentTime = animState.normalizedTime;
        Debug.Log("Time: " + animState.normalizedTime);
        return currentTime;
    }

    public float GetDurationOfAnimatorClip(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        float duration = animState.length;
        return duration;
    }
}
