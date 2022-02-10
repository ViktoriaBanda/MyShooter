using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;
    
    [SerializeField]
    private Scrollbar _healthBar;
    
    [SerializeField] 
    private Player _player;

    public void Initialize(float currentHealth, Color color)
    {
        _healthBar.size = currentHealth / _characteristicManager.GetCharacteristicByName("Health").GetMaxValue();

        var healthBarColors = _healthBar.colors;
        healthBarColors.disabledColor = color;
        _healthBar.colors = healthBarColors;
    }
}
