using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Scrollbar _healthBar;
    
    [SerializeField]
    private HealthController _healthController;

    private float _currentHealth;
    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _healthController.MaxHealth;
        _healthController.CurrentHealth.Do(value => Initialize(value)).Subscribe().AddTo(this);
    }


    private void Initialize(float currentHealth)
    {
        switch (currentHealth)
        {
            case <= 0:
                _healthBar.gameObject.SetActive(false);
                return;
            case >= 20:
                SetHealth(currentHealth, Color.green);
                _healthBar.gameObject.SetActive(true);
                return;
            case >= 10:
                SetHealth(currentHealth, Color.yellow);
                return;
            default:
                SetHealth(currentHealth, Color.red);
                break;
        }
    }

    private void SetHealth(float currentHealth, Color color)
    {
        _healthBar.size = currentHealth / _maxHealth;

        var healthBarColors = _healthBar.colors;
        healthBarColors.disabledColor = color;
        _healthBar.colors = healthBarColors;
    }
}
