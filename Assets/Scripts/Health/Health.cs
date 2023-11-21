using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float _damage, Collider2D other)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);
        if (currentHealth > 0)
        {
            //Animacja dostawania dmg
            if (other.CompareTag("Player"))
            {

            }
            if (other.CompareTag("Enemy"))
            {

            }
        }
        else if (currentHealth <= 0)
        {
            //Animacja umierania
            if (other.CompareTag("Player"))
            {
                player.isDead = true;
            }
            if (other.CompareTag("Enemy"))
            {
                enemy.isDead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }
}
