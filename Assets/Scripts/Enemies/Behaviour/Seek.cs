using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Seek : Action
{
    public Player Target;
    public NavMeshAgent NavMeshAgent;
        
    public override TaskStatus OnUpdate()
    {
        // Return a task status of success once we've reached the target
        if (Vector3.Distance(transform.position,Target.transform.position) <= 1f) 
        {
            return TaskStatus.Success;
        }
            
        // We haven't reached the target yet so keep moving towards it
        NavMeshAgent.destination = Target.transform.position;
        return TaskStatus.Running;
    }
}
