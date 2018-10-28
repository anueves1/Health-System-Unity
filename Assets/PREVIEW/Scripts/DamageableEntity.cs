using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public AudioClip[] DestroyClips => m_DestroyClips;

    [SerializeField]
    private float m_MaxHealth = 100;

    [Header("On Death")]

    [SerializeField]
    private Item[] m_ItemDrops;

    [SerializeField]
    private float m_SpawnRange = 2f;

    [SerializeField]
    private AudioClip[] m_DestroyClips;

    [SerializeField]
    private GameObject m_DestroyParticles;

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
        GameObject particles = Instantiate(m_DestroyParticles, transform.position + Vector3.up, transform.rotation);

        DropItems();
    }

    private void DropItems()
    {
        //Loop trough the items.
        for(var i = 0; i < m_ItemDrops.Length; i++)
        {
            //Get the current item to drop.
            Item current = m_ItemDrops[i];

            //Loop trough the amount of items we need to spawn.
            for(var p = 0; p < current.Amount; p++)
            {
                //Get a random value to offset the item drop by.
                Vector3 offset = Random.insideUnitSphere * m_SpawnRange;
                //Add the y offset.
                offset.y = current.YOffset;

                //Get the position with the added offset.
                Vector3 dropPosition = transform.position + offset;

                //Instantiate the item.
                Instantiate(current.Prefab, dropPosition, current.Prefab.transform.rotation);
            }
        }
    }

    public bool IsDead() => m_CurrentHealth <= 0;
}