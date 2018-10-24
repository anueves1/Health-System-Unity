using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public AudioClip[] DestroyClips { get { return m_DestroyClips; } }

    [SerializeField]
    private float m_MaxHealth = 100;

    [Header("On Death")]

    [SerializeField]
    private AudioClip[] m_DestroyClips;

    [SerializeField]
    private GameObject m_DestroyParticles;

    [SerializeField]
    private float m_ParticleDestroyTime = 5f;

    private float m_CurrentHealth;

    private void Awake()
    {
        //Start with max health.
        m_CurrentHealth = m_MaxHealth;
    }

    public void Damage(float damage)
    {
        //Substract damage.
        m_CurrentHealth -= damage;

        //If the health is below zero, this entity is dead.
        if (IsDead())
            HandleDeath();
    }

    private void HandleDeath()
    {
        //Destroy this object.
        Destroy(gameObject);

        //Spawn destroy particles.
        GameObject particles = Instantiate(m_DestroyParticles, transform.position, transform.rotation);

        //Destroy them after some time.
        Destroy(particles, m_ParticleDestroyTime);
    }

    public bool IsDead() { return m_CurrentHealth <= 0; }
}