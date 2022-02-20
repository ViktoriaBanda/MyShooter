using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        EventStreams.Game.Publish(new GameSceneLoadedEvent());
    }
}
