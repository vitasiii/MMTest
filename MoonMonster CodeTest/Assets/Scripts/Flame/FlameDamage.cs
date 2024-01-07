using MoonMonster.Codetest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDamage : MonoBehaviour
{
    #region Editor Fields

    [SerializeField]
    private float DamagePerSecond = 15f;

    #endregion

    #region Fields

    private GameObject _shooter;

    private List<TankHealth> _affectedTanks = new List<TankHealth>();

    #endregion

    #region Properties

    public GameObject Shooter
    {
        get { return _shooter; }
        set { _shooter = value; }
    }

    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Shooter) return;

        TankHealth targetHealth = other.GetComponent<TankHealth>();

        if (!targetHealth)
            return;

        _affectedTanks.Add(targetHealth);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Shooter) return;

        TankHealth targetHealth = other.GetComponent<TankHealth>();

        if (!targetHealth)
            return;

        if (!_affectedTanks.Contains(targetHealth))
        {
            Debug.LogError("An unregistred tank just left flames!");
            return;
        }

        _affectedTanks.Remove(targetHealth);
    }

    private void Update()
    {
        HandleFlamingTanks();
    }

    private void HandleFlamingTanks()
    {
        foreach (TankHealth tank in _affectedTanks)
        {
            tank.TakeDamage(DamagePerSecond * Time.deltaTime);
        }
    }
}
