using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public string Name => GlobalConstants.HEALTH;

    [SerializeField] 
    private CharacteristicManager _characteristicManager;
    
    [SerializeField]
    private Scrollbar _healthBar;

    public void Initialize(float currentHealth, Color color)
    {
        _healthBar.size = currentHealth / _characteristicManager.GetCharacteristicByName(Name).GetMaxValue();

        var healthBarColors = _healthBar.colors;
        healthBarColors.disabledColor = color;
        _healthBar.colors = healthBarColors;
    }
}
