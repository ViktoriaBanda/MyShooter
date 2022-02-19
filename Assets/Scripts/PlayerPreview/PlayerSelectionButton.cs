using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionButton : MonoBehaviour
{
    [SerializeField]
    private Image _playerIcon;
   
    private PlayerSettings _player;
    
    public void Initialize(PlayerSettings player)
    {
        _player = player;
        _playerIcon.sprite = player.Icon;
    }
    
    public void OnClick()
    {
        PrefsManager.SaveLastSelectedPlayer(_player.Name);
        EventStreams.Game.Publish(new PlayerSelectionEvent(_player));
    }
}
