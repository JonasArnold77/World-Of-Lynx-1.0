using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Forward,
    Right,
    Left,
    Back
}


public class EnemyAnimation : MonoBehaviour
{
    public AnimationClip Hit;

    private Animator _Animator;


    public Vector3 StartPointOfHit;
    public Vector3 EndPointOfHit;

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

    public void GetHitDirection(Vector3 hitVector)
    {
        hitVector = new Vector3(hitVector.x, hitVector.y, 0);

        var pAngle = Vector3.Angle(Attack.Instance.gameObject.transform.position - hitVector, transform.position);

        var angle = Vector3.Angle(hitVector, transform.forward);
    }
}
