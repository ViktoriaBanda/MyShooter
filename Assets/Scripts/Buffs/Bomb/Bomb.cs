using System.Collections.Generic;
using UnityEngine;

public class Bomb : Buff
{
    [SerializeField] 
    private BombTargets _bombTargets;
    
    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            var targets = _bombTargets.Enemies;
            
            EventStreams.Game.Publish(new BombExplodeEvent(gameObject));
            KillAllTargets(targets);
        }
    }

    private void KillAllTargets(List<GameObject> targets)
    {
        foreach (var target in targets)
        {
            EventStreams.Game.Publish(new EnemyTakesDamageEvent(target));
        }
    }
}
