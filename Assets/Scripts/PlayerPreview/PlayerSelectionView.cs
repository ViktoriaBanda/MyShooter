using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionView : MonoBehaviour
{
    [SerializeField]
    private PlayerSettingsProvider _playerSettingsProvider;

    [SerializeField]
    private PlayerSelectionButtons _playerSelectionButtons;

    private void Start()
    {
        _playerSelectionButtons.Initialize(_playerSettingsProvider);
    }
}
