using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonMonster.Codetest
{
    public class AIController : MonoBehaviour
    {
        public float AggressionDistance = 15;

        public float _maxMovementTime = 3f;
        public float _minMovementTime = 1.25f;

        private TankShooting _shooting;
        private TankMovement _movement;
        
        private string _movementAxisName;
        private string _turnAxisName;
        private string _fireButtonName;

        public Transform Target { get; set; }
        
        void Start()
        {
            _shooting = gameObject.GetComponent<TankShooting>();
            _movement = gameObject.GetComponent<TankMovement>();
        }

        public void Reset()
        {
            StartCoroutine(StartAIMoving());
        }

        void Update()
        {
            HandleAIShooting();
        }

        private void HandleAIShooting()
        {
            _shooting.LookAtTarget(Target);

            if (Vector3.Distance(transform.position, Target.position) < AggressionDistance)
            {
                _shooting.Fire();
            }
        }

        private IEnumerator StartAIMoving()
        {
            while (true)
            {
                if (_movement)
                    yield return StartCoroutine(AIMoveRandomDirForSeconds(Random.Range(_minMovementTime, _maxMovementTime)));
                else yield return null;
            }
        }

        private IEnumerator AIMoveRandomDirForSeconds(float seconds)
        {
            float timer = seconds;
            float verticalMovement = Random.Range(-1f, 1f);
            float horizontalMovement = Random.Range(-1f, 1f);

            while (timer > 0)
            {
                _movement.SetMoveInput(verticalMovement, horizontalMovement);
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }
}