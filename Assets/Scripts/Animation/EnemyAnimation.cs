using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public AnimationClip Hit;

    private Animator _Animator;

    private void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    public void PlayingHitAnimation()
    {
       if (_Animator.GetBool("Hit"))
        {
            _Animator.SetBool("InterruptHit", true);
        }

        _Animator.SetBool("Hit", true);
    }
}
