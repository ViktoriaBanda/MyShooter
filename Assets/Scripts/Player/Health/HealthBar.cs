using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;

    [SerializeField]
    private Scrollbar _healthBar;

    private float _currentHealth;

    private void Update()
    {
        _currentHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).GetCurrentValue();
        
        switch (_currentHealth)
        {
            case <= 0:
                _healthBar.gameObject.SetActive(false);
                return;
            case >= 20:
                Initialize(_currentHealth, Color.green);
                _healthBar.gameObject.SetActive(true);
                return;
            case >= 10:
                Initialize(_currentHealth, Color.yellow);
                return;
            default:
                Initialize(_currentHealth, Color.red);
                break;
        }
    }

    private void Initialize(float currentHealth, Color color)
    {
        _healthBar.size = currentHealth / _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).GetMaxValue();

        var healthBarColors = _healthBar.colors;
        healthBarColors.disabledColor = color;
        _healthBar.colors = healthBarColors;
    }
}
