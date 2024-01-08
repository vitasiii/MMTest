using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoonMonster.Codetest
{
    // Startegy pattern: Context
    public class TankShooting : MonoBehaviour
    {
        // The Weapons
        [SerializeField]
        private List<BaseTankWeaponScriptableObject> _weapons;
        [SerializeField]
        private Transform _fireTransform;
        [SerializeField]
        private AudioSource _shootingAudio;

        private BaseTankWeaponScriptableObject _currentWeapon;
        private int _currentWeaponSlot = 0;

        // Shooting cooldown
        public UnityEvent<float> OnReloadCountdownChanged;
        private float _reloadCountdown;
        private bool _fired;

        // Camera stuff
        private Camera _camera;
        public GameObject Turret;
        public float AngleOffset = 90f;
        public bool LookAtMouse = false;

        // Audio
        private bool _playingShootinAudioLoop = false;

        private void Awake()
        {
            if (_weapons.Count > 0)
                _currentWeapon = _weapons[_currentWeaponSlot];
            else
            {
                Debug.LogError("No weapons set for this tank: " + this);
            }
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Reset()
        {
            _reloadCountdown = 0f;
            OnReloadCountdownChanged.Invoke(_reloadCountdown);
        }

        private void Update()
        {
            if (LookAtMouse == true)
                LookAtMousePosition();

            if (_fired)
            {
                if (_reloadCountdown <= 0)
                    _fired = false;
                else
                    _reloadCountdown -= Time.deltaTime;

                OnReloadCountdownChanged.Invoke(_reloadCountdown);
            }
        }

        public void LookAtTarget(Transform target)
        {
            if (target == null)
                return;

            var dir = target.position - Turret.transform.position;
            dir.y = 0;
            dir.Normalize();

            var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg - AngleOffset;
            Turret.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
        }

        public void Fire()
        {
            if (_fired)
                return;

            _currentWeapon.FireWeapon(gameObject, _fireTransform);

            if (!_playingShootinAudioLoop)
            {
                if (_currentWeapon.FireClip && !_currentWeapon.FireClipLoop)
                {
                    _shootingAudio.clip = _currentWeapon.FireClip;
                    _shootingAudio.loop = false;
                    _shootingAudio.Play();
                }
                else if (!_currentWeapon.FireClip && _currentWeapon.FireClipLoop)
                {
                    _shootingAudio.clip = _currentWeapon.FireClipLoop;
                    _playingShootinAudioLoop = true;
                    _shootingAudio.loop = true;
                    _shootingAudio.Play();
                }
                else
                    Debug.LogError("Shooting Audio Error! Either both FireClip and FireClipLoop set or not set");
            }
            
            if (_currentWeapon.FireDelay > 0)
            {
                _fired = true;
                _reloadCountdown = _currentWeapon.FireDelay;
            }
        }

        public void StopFire()
        {
            _currentWeapon.StopFiringWeapon();

            if (_playingShootinAudioLoop)
            {
                _shootingAudio.Stop();
                _playingShootinAudioLoop = false;
            }
        }

        public void SelectWeaponSlot(int slot)
        {
            StopFire();
            if (slot >= _weapons.Count)
            {
                Debug.LogError("There is no weapon for such slot");
                return;
            }

            // Play a sound maybe
            _currentWeaponSlot = slot;
            _currentWeapon = _weapons[_currentWeaponSlot];
        }

        public void ScrollThroughWeapons(bool next)
        {
            StopFire();
            if (next)
            {
                if (_currentWeaponSlot + 1 < _weapons.Count)
                    _currentWeaponSlot++;
                else
                    _currentWeaponSlot = 0;
            }
            else
            {
                if (_currentWeaponSlot == 0)
                    _currentWeaponSlot = _weapons.Count - 1;
                else
                    _currentWeaponSlot--;
            }
            _currentWeapon = _weapons[_currentWeaponSlot];
        }

        private void LookAtMousePosition()
        {
            Vector3 mousePos = Input.mousePosition;
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(mousePos);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPos = ray.GetPoint(distance);
                var dir = targetPos - Turret.transform.position;
                dir.y = 0;
                dir.Normalize();

                var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg - AngleOffset;
                Turret.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            }
        }
    }
}