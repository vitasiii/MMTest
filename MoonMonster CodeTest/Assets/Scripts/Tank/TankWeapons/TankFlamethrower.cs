using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Strategy pattern: strategy instance
[CreateAssetMenu(fileName = "TankFlamethrower", menuName = "TankWeapons/TankFlamethrower")]
public class TankFlamethrower : BaseTankWeaponScriptableObject
{
    #region Editor Fields

    [SerializeField]
    private GameObject _flamePrefab;

    #endregion

    #region Fields

    private GameObject _currentFlame = null;

    #endregion

    #region Methods

    public override void FireWeapon(GameObject shooter, Transform fireTransform)
    {
        if (_currentFlame) return;

        _currentFlame = Instantiate(_flamePrefab, fireTransform);
    }

    public override void StopFiringWeapon()
    {
        Destroy(_currentFlame);
        _currentFlame = null;
    }

    #endregion
}
