using BehaviorDesigner.Runtime.Tasks;
using SimpleEventBus.Disposables;

public class Observe : Action
{
    public EnemyAgrRegion EnemyAgrRegion;
    private bool _wasPlayerFound;
    
    private CompositeDisposable _subscriptions;
    
    public override void OnAwake()
    {
        EnemyAgrRegion.OnPlayerGetIntoAgrRegion += EndObserve;
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    public override TaskStatus OnUpdate()
    {
        if (_wasPlayerFound) 
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _wasPlayerFound = false;
    }

    private void EndObserve()
    {
        _wasPlayerFound = true;
    }

    public override void OnReset()
    {
        _subscriptions.Dispose();
        EnemyAgrRegion.OnPlayerGetIntoAgrRegion -= EndObserve;
    }
}

