using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image maxHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start()
    {
        maxHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
