using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public Transform TipOfTheSword;

    private void Start()
    {
        TipOfTheSword = gameObject.transform.GetChild(0);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {

    //        other.GetComponent<EnemyAnimation>().PlayingHitAnimation();
    //        other.GetComponent<EnemyAnimation>().StartPointOfHit = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        //other.GetComponent<EnemyAnimation>().EndPointOfHit = o;
    //        other.GetComponent<EnemyAnimation>().GetHitDirection(other.transform.forward);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            collision.gameObject.GetComponent<EnemyAnimation>().StartPointOfHit = collision.contacts[0].point;
        } 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<EnemyAnimation>().EndPointOfHit = collision.contacts[0].point;
            //collision.gameObject.GetComponent<EnemyAnimation>().GetHitDirection(collision.gameObject.transform.forward);
            collision.gameObject.GetComponent<EnemyAnimation>().PlayingHitAnimation();


            Attack.Instance.EndPoint = WeaponManager.Instance.ActualWeapon.TipOfTheSword.transform.position;
            Attack.Instance.GetSwordDirection(collision.gameObject.GetComponent<EnemyAnimation>());
        }
    }
}
