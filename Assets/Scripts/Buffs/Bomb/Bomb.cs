using UnityEngine;

public class Bomb : Buff
{
    [SerializeField] 
    private Targets _bombTargets;
    
    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            EventStreams.Game.Publish(new BombExplodeEvent(gameObject));
            KillAllTargets();
        }
    }

    private void KillAllTargets()
    {
        var targets = _bombTargets.Enemies;

        while (targets.Count > 0)
        {
           EventStreams.Game.Publish(new EnemyTakesDamageEvent(targets[^1]));
        }
    }
}
