using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Scrollbar _healthBar;
    
    [SerializeField] 
    private Player _player;

    public void Initialize(float currentHealth, Color color)
    {
        _healthBar.size = currentHealth / _player.GetMaxHealth();

        var healthBarColors = _healthBar.colors;
        healthBarColors.disabledColor = color;
        _healthBar.colors = healthBarColors;
    }
}
