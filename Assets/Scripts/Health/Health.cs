using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Player player;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);
        if (currentHealth > 0)
        {
            //Animacja dostawania dmg
        }
        else
        {
            //Animacja umierania
            player.isDead = true;
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }
}

