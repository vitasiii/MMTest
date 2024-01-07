using MoonMonster.Codetest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Strategy pattern: strategy instance
[CreateAssetMenu(fileName = "TankCannon", menuName = "TankWeapons/TankCannon")]
public class TankCannon : BaseTankWeaponScriptableObject
{
    #region Editor Fields

    [SerializeField]
    protected Rigidbody _projectile;
    [SerializeField]
    protected float _launchForce = 15f;

    #endregion

    #region Methods

    public override void FireWeapon(GameObject shooter, Transform fireTransform)
    {
        Rigidbody shellInstance =
               Instantiate(_projectile, fireTransform.position, fireTransform.rotation) as Rigidbody;

        shellInstance.velocity = _launchForce * fireTransform.forward;

        shellInstance.GetComponent<ShellExplosion>().Shooter = shooter;
    }

    public override void StopFiringWeapon()
    {
    }

    #endregion
}
