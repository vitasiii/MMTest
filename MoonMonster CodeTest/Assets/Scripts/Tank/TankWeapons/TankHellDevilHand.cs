using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonMonster.Codetest;


[CreateAssetMenu(fileName = "TankHellDevilHand", menuName = "TankWeapons/TankHellDevilHand")]
public class TankHellDevilHand : BaseTankWeaponScriptableObject
{
    #region Editor Fields

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _raycastDistance = 100f;

    [SerializeField]
    private GameObject _hellDevilHandPrefab;

    [SerializeField]
    private float _healthPrice = 35f;

    #endregion

    #region Methods

    public override void FireWeapon(GameObject shooter, Transform fireTransform)
    {
        TankHealth shooterHealth = shooter.GetComponent<TankHealth>();

        if (shooterHealth.CurrentHealth - _healthPrice <= 0)
        {
            Instantiate(_hellDevilHandPrefab).GetComponent<HellDevilHandLogic>().StartFollowingObject(shooter);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ( Physics.Raycast(ray, out hit, _raycastDistance, _layerMask))
        {
            Instantiate(_hellDevilHandPrefab, hit.point, Quaternion.identity);
            shooterHealth.TakeDamage(_healthPrice);
        }
    }

    public override void StopFiringWeapon()
    {
    }

    #endregion
}
