using UnityEngine;

public class Buff : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            EventStreams.Game.Publish(new BuffAchieveEvent(this));
            gameObject.SetActive(false);
        }
    }
}
