using UnityEngine;

public class BuffSound : MonoBehaviour
{
    [SerializeField]
    protected AudioSource _audioSource;

    [SerializeField] 
    protected Buff _buff;

    private void Awake()
    {
        _buff.BuffAchieveEvent += BuffAchieveEventHandler;
    }

    private void BuffAchieveEventHandler()
    {
        _audioSource.Play();
    }
    
    private void OnDestroy()
    {
        _buff.BuffAchieveEvent -= BuffAchieveEventHandler;
    }
}
