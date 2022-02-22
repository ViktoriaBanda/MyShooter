using BehaviorDesigner.Runtime.Tasks;
using SimpleEventBus.Disposables;

public class Dead : Action
{
    private CompositeDisposable _subscriptions;
    private bool _isDead;
    
    public override void OnAwake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler)
        };
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_isDead) 
        {
            return TaskStatus.Failure;
        }
        
        return TaskStatus.Success;
    }
    
    private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
    {
        if (eventData.Enemy.gameObject == gameObject)
        {
            gameObject.SetActive(false);
            _isDead = true;
        }
    }
    
    public override void OnReset()
    {
        _subscriptions.Dispose();
    }
}
