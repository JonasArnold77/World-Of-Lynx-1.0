using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _Animator;
    public static PlayerAnimation Instance;

    private void Awake()
    {
        Instance = this;
    }

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

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    _Animator.Play("Melee Attack 1");
        //}
    }

    public void PlayNextAttack(EControls control)
    {
        var combo = ComboManager.Instance.Combos.Where(c => c.InputType == control).FirstOrDefault();
        _Animator.Play(combo.ComboList[combo.Counter].name);
        combo.IncreaseCounter();
    }

    public void Attack()
    {

    }
}
