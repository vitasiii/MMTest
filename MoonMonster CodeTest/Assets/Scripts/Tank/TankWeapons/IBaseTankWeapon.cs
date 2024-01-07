using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Startegy pattern: BaseStrategy
public interface IBaseTankWeapon
{
    void FireWeapon(GameObject shooter, Transform fireTransform);
    void StopFiringWeapon();
}
