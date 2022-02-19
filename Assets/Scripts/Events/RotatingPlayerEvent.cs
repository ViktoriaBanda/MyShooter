using SimpleEventBus.Events;

public class RotatingPlayerEvent : EventBase
{
    public float RotationY { get; }
    
    public RotatingPlayerEvent(float rotationY)
    {
        RotationY = rotationY;
    }
}