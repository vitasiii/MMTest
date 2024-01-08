using MoonMonster.Codetest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellDevilHandGrabArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TankHealth targetHealth = other.GetComponent<TankHealth>();

        if (!targetHealth)
            return;

        // Assuming that the victim has neither any support nor contracts to get them out of hell
        targetHealth.TakeDamage(float.MaxValue);
    }
}
