using SimpleEventBus.Events;

public class PlayerSelectionEvent : EventBase
{
    public PlayerSettings SelectedPlayer { get; }
    public PlayerSelectionEvent(PlayerSettings player)
    {
        SelectedPlayer = player;
    }
}