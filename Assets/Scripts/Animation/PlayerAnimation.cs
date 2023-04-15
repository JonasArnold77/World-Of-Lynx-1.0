using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0.01f || Input.GetAxis("Horizontal") < -0.01f || Input.GetAxis("Vertical") > 0.01f || Input.GetAxis("Vertical") < -0.01f)
        {
            _Animator.SetBool("Run", true);
        }
        else
        {
            _Animator.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _Animator.Play("Melee Attack 1");
        }
    }

    public void Attack()
    {

    }
}
