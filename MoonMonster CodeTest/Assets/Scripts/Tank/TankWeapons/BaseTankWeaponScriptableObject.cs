using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTankWeaponScriptableObject : ScriptableObject, IBaseTankWeapon
{
    #region Editor Fields

    [SerializeField]
    protected float _fireDelay = 0.1f;

    [Header("Only one of these AudioClips will be played!")]
    [SerializeField]
    private AudioClip _fireClip;
    [SerializeField]
    private AudioClip _fireClipLoop;

    #endregion

    #region Properties

    public float FireDelay => _fireDelay;
    
    public AudioClip FireClip => _fireClip;
    public AudioClip FireClipLoop => _fireClipLoop;

    #endregion

    #region Methods

    public abstract void FireWeapon(GameObject shooter, Transform fireTransform);
    public abstract void StopFiringWeapon();

    #endregion

}
