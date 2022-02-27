using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Attack : Action
{
    public Seek Seek;
    public float DamageValue = 5f;
    
    private Player _target;
    
    public override void OnAwake()
    {
        _target = Seek.Target;
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position,_target.transform.position) > 2f) 
        {
            return TaskStatus.Success;
        }

        EventStreams.Game.Publish(new PlayerTakesDamageEvent(DamageValue));
        return TaskStatus.Success;
    }
}
