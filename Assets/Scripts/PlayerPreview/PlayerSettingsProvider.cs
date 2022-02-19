using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingsProvider", menuName = "PlayerSettings", order = 51)]
public class PlayerSettingsProvider : ScriptableObject
{
    [SerializeField]
    private PlayerSettings[] _playerSettings;
    
    private readonly Dictionary<string, PlayerSettings> _playerSettingByName = new();
    
    public void Initialize()
    {
        foreach (var playerSettings in _playerSettings)
        {
            _playerSettingByName[playerSettings.Name] = playerSettings;
        }
    }
    
    public PlayerSettings[] GetAllPlayers()
    {
        return _playerSettings;
    }
    
    public PlayerSettings GetPlayer(string playerName)
    {
        return _playerSettingByName[playerName];
    }
}

