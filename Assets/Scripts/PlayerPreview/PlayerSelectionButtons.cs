using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pool;
using UnityEngine;

public class PlayerSelectionButtons : MonoBehaviour
{
    [SerializeField]
    private PlayerSelectionButton _button;

    [SerializeField]
    private RectTransform _buttonsRoot;

    private MonoBehaviourPool<PlayerSelectionButton> _selectionButtonsPool;

    private void Awake()
    {
        _selectionButtonsPool = new MonoBehaviourPool<PlayerSelectionButton>(_button, _buttonsRoot, 3);
    }

    public void Initialize(PlayerSettingsProvider settingsProvider)
    {
        var players = settingsProvider.GetAllPlayers();
        foreach (var player in players)
        {
            var playerSelectionButton = _selectionButtonsPool.Take();
            playerSelectionButton.Initialize(player);
        }

        SelectLastSelectedPlayer(settingsProvider);
    }

    private void SelectLastSelectedPlayer(PlayerSettingsProvider settingsProvider)
    {
        var hasSelectedPlayer = string.IsNullOrEmpty(PrefsManager.GetLastSelectedPlayer());
        var lastSelectedPlayer = hasSelectedPlayer ?
            settingsProvider.GetAllPlayers().First() :
            settingsProvider.GetPlayer(PrefsManager.GetLastSelectedPlayer());
        
        EventStreams.Game.Publish(new PlayerSelectionEvent(lastSelectedPlayer));
    }
}
