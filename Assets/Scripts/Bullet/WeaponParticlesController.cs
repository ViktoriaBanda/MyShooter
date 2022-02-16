using UnityEngine;

public class WeaponParticlesController : MonoBehaviour
{
    [SerializeField]
    private GameObject _particle;
    
    [SerializeField] 
    private Weapon _weapon;

    private void Awake()
    {
        _weapon.OnShooting += PlayParticles;
    }
    
    private void PlayParticles()
    {
        _particle.transform.position = transform.position;
         
        var particles = _particle.GetComponent<ParticleSystem>();
        particles.Play();
    }
    
    private void OnDestroy()
    {
        _weapon.OnShooting -= PlayParticles;
    }
}
