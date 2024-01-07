using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTankWeaponScriptableObject : ScriptableObject, IBaseTankWeapon
{
    #region Editor Fields

    [SerializeField]
    private AudioClip _fireClip;

    [SerializeField]
    protected float _fireDelay = 0.1f;

    #endregion

    #region Properties

    public float FireDelay => _fireDelay;
    public AudioClip FireClip => _fireClip;

    #endregion

    #region Methods

    public abstract void FireWeapon(GameObject shooter, Transform fireTransform);
    public abstract void StopFiringWeapon();

    #endregion

}
