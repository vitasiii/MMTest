using UnityEngine;

namespace MoonMonster.Codetest
{
    public class ShellExplosion : MonoBehaviour
    {
        #region Editor Fields

        [SerializeField]
        private LayerMask TankMask;
        [SerializeField]
        private ParticleSystem ExplosionParticles;
        [SerializeField]
        private AudioSource ExplosionAudio;
        [SerializeField]
        private float MaxDamage = 100f;
        [SerializeField]
        private float ExplosionForce = 1000f;
        [SerializeField]
        private float MaxLifeTime = 2f;
        [SerializeField]
        private float ExplosionRadius = 5f;

        #endregion

        #region Fields

        private GameObject _shooter;

        #endregion

        #region Properties

        public GameObject Shooter
        {
            get { return _shooter; }
            set { _shooter = value; }
        }

        #endregion

        #region Methods

        private void Start()
        {
            Destroy(gameObject, MaxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Shooter) return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, TankMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                if (!targetRigidbody)
                    continue;

                targetRigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

                if (!targetHealth)
                    continue;

                float damage = CalculateDamage(targetRigidbody.position);

                targetHealth.TakeDamage(damage);
            }

            ExplosionParticles.transform.parent = null;

            ExplosionParticles.Play();

            ExplosionAudio.Play();

            ParticleSystem.MainModule mainModule = ExplosionParticles.main;
            Destroy(ExplosionParticles.gameObject, mainModule.duration);

            Destroy(gameObject);
        }


        private float CalculateDamage(Vector3 targetPosition)
        {
            Vector3 explosionToTarget = targetPosition - transform.position;

            float explosionDistance = explosionToTarget.magnitude;

            float relativeDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;

            float damage = relativeDistance * MaxDamage;

            damage = Mathf.Max(0f, damage);

            return damage;
        }
    }
    
    #endregion
}