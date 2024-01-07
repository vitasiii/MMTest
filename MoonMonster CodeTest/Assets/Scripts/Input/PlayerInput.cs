using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonMonster.Codetest
{
    public class PlayerInput : MonoBehaviour
    {
        public int PlayerNumber = 1; 
        private TankMovement _movement;
        private TankShooting _shooting;

        
        private string _movementAxisName;         
        private string _turnAxisName;    
        private string _fireButtonName;
        private string _weaponSlot1ButtonName;
        private string _weaponSlot2ButtonName;
        private string _weaponSlot3ButtonName;
        private string _scrollSelectWeaponButtonName;


        void Start()
        {
            _movement = gameObject.GetComponent<TankMovement>();
            _shooting = gameObject.GetComponent<TankShooting>();
            
            _movementAxisName = "Vertical" + PlayerNumber;
            _turnAxisName = "Horizontal" + PlayerNumber;
            _fireButtonName = "Fire" + PlayerNumber;
            _weaponSlot1ButtonName = "WeaponSlot1" + PlayerNumber;
            _weaponSlot2ButtonName = "WeaponSlot2" + PlayerNumber;
            _weaponSlot3ButtonName = "WeaponSlot3" + PlayerNumber;
            _scrollSelectWeaponButtonName = "ScrollSelectWeapon" + PlayerNumber;
        }

        void Update()
        {
            HandleMovement();
            HandleShooting();
            HandleWeaponSwitch();
        }

        private void HandleMovement()
        {
            _movement.SetMoveInput(Input.GetAxis(_movementAxisName), Input.GetAxis(_turnAxisName));
        }

        private void HandleShooting()
        {
            if (Input.GetButton(_fireButtonName))
            {
                _shooting.Fire();
            }
            else if (Input.GetButtonUp(_fireButtonName))
            {
                _shooting.StopFire();
            }
        }

        private void HandleWeaponSwitch()
        {
            if (Input.GetButton(_weaponSlot1ButtonName))
                _shooting.SelectWeaponSlot(0);

            if (Input.GetButton(_weaponSlot2ButtonName))
                _shooting.SelectWeaponSlot(1);

            if (Input.GetButton(_weaponSlot3ButtonName))
                _shooting.SelectWeaponSlot(2);

            if (Input.GetAxis(_scrollSelectWeaponButtonName) > 0)
                _shooting.ScrollThroughWeapons(true);
            else if (Input.GetAxis(_scrollSelectWeaponButtonName) < 0)
                _shooting.ScrollThroughWeapons(false);
        }
    }

}