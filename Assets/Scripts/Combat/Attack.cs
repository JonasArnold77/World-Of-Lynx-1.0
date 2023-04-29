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

    private bool _IsPlayingAttack;

    private Coroutine _WaitForResettingCoroutine�nstance;
    private Coroutine _AttackIsHappenCoroutineInstance;

    private bool _CheckForComboTimeWindowCorutineIsRunning;
    private bool _WaitForResettingCoroutineIsActive;
    private bool _ComboCoroutineIsRunning;

    private void Start()
    {
        InventoryManager.Instance.ActualWeapon.GetComponent<Collider>().enabled = false;
        _Animator = GetComponent<Animator>();


    }

    private void Update()
    {
        if (!_ComboCoroutineIsRunning)
        {
            StartCoroutine(CalculateCombo());
        }
    }

    public IEnumerator CalculateCombo()
    {
        _ComboCoroutineIsRunning = true;
        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.LightHit)) && !CheckIfPossibleToAttack())
        {
            ComboManager.Instance.ResetAllComboCountersInsteadOfSelected(EControls.LightHit);
            if (_WaitForResettingCoroutineIsActive)
            {
                StopCoroutine(_WaitForResettingCoroutine�nstance);
            }

            //have to be before attack coroutine
            _WaitForResettingCoroutine�nstance = StartCoroutine(WaitForResettingCoroutine(EControls.LightHit));
            yield return StartCoroutine(PlayerAnimation.Instance.PlayNextAttack(EControls.LightHit));

            ComboManager.Instance.CheckForSuperCombo(EControls.LightHit);
        }

        if (Input.GetKeyDown((KeyCode)InputManager.Instance.GetInputActionFromControlInput(EControls.HardHit)) && !CheckIfPossibleToAttack())
        {
            ComboManager.Instance.ResetAllComboCountersInsteadOfSelected(EControls.HardHit);
            if (_WaitForResettingCoroutineIsActive)
            {
                StopCoroutine(_WaitForResettingCoroutine�nstance);
            }
            
            //have to be before attack coroutine
            _WaitForResettingCoroutine�nstance = StartCoroutine(WaitForResettingCoroutine(EControls.HardHit));
            yield return StartCoroutine(PlayerAnimation.Instance.PlayNextAttack(EControls.HardHit));

            ComboManager.Instance.CheckForSuperCombo(EControls.HardHit);
        }
        _ComboCoroutineIsRunning = false;
    }

    private IEnumerator WaitForResettingCoroutine(EControls control)
    {
        _WaitForResettingCoroutineIsActive = true;
        yield return new WaitForSeconds(2f);
        ComboManager.Instance.Combos.Where(c => c.InputType == control).FirstOrDefault().ResetCounter();
        InputManager.Instance.CollectingInputsList.Clear();
        _WaitForResettingCoroutineIsActive = false;
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
        if (!_CheckForComboTimeWindowCorutineIsRunning)
        {
            _CheckForComboTimeWindowCorutineIsRunning = true;

            //if I hit before the combo countr should be resetted
            //yield return new WaitWhile

            yield return new WaitUntil(() => GetCurrentAnimatorTime() >= GetDurationOfAnimatorClip() / 2);
            IsInComboTimeWindow = true;
            yield return new WaitUntil(() => GetCurrentAnimatorTime()>=GetDurationOfAnimatorClip());

            yield return new WaitForSeconds(0.5f);
            IsInComboTimeWindow = false;
            ComboManager.Instance.ResetComboCounter();
            FirstHit = false;
            _CheckForComboTimeWindowCorutineIsRunning = false;
        }
    }

    public bool CheckIfPossibleToAttack(int layer = 1)
    {
        AnimatorStateInfo animState = _Animator.GetCurrentAnimatorStateInfo(layer);
        if (animState.IsTag("Attack"))
        {
            if(animState.normalizedTime > animState.length * 0.6f)
            {
                _IsPlayingAttack = false;
                return false;
            }

            _IsPlayingAttack = true;
            return true;
        }
        else
        {
            _IsPlayingAttack = false;
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
