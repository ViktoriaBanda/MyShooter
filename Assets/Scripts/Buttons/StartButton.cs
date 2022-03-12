using UnityEngine;

public class StartButton : MonoBehaviour
{
    private void Awake()
    {
        EventStreams.Game.Publish(new LobbySceneLoadedEvent());
    }
}