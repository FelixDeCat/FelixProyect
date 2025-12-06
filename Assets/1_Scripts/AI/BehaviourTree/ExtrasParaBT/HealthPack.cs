using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] int healQuant;
    [SerializeField] ParticleSystem particle;

    public int HealAction()
    {
        particle.Play();
        return healQuant;
    }
}
