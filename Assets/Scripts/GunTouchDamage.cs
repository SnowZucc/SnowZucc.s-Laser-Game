using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTouchDamage : MonoBehaviour
{
    public float damagePerSecond = 10f;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}