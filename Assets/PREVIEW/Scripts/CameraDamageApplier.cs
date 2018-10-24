using UnityEngine;

public class CameraDamageApplier : MonoBehaviour
{
    [SerializeField]
    private float m_Damage = 20;

    [SerializeField]
    private AudioClip[] m_HitClips;

    [Header("Particles")]

    [SerializeField]
    private float m_ParticlesOffset = 2;

    [SerializeField]
    private GameObject m_HitParticles;

    [SerializeField]
    private float m_ParticleDestroyTime;

    private AudioSource m_Source;

    private void Awake()
    {
        //Get the source.
        m_Source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Make a ray towards the position the mouse is pointing at.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            //Try to get the damageable entity component.
            DamageableEntity entity = hit.transform.GetComponent<DamageableEntity>();

            //If the object we hit has that component.
            if (entity)
            {
                //If we click.
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Apply damage.
                    entity.Damage(m_Damage);

                    //If that last hit destroyed the object, return.
                    if (entity.IsDead())
                    {
                        //Get the random index for the clip to play.
                        var rndDestroyClip = Random.Range(0, entity.DestroyClips.Length);

                        //Assign the clip.
                        m_Source.clip = entity.DestroyClips[rndDestroyClip];

                        //Play it.
                        m_Source.Play();

                        return;
                    }

                    //Get the direction to the point.
                    Vector3 dirToPoint = (hit.point - transform.position).normalized;

                    //Spawn point for particles.
                    Vector3 spawnPoint = transform.position + (dirToPoint * m_ParticlesOffset);

                    //Spawn some particles.
                    GameObject hitParts = Instantiate(m_HitParticles, spawnPoint, Quaternion.identity);

                    //Destroy them after some time.
                    Destroy(hitParts, m_ParticleDestroyTime);

                    //Get the random index for the clip to play.
                    var randomClip = Random.Range(0, m_HitClips.Length);

                    //Assign the clip.
                    m_Source.clip = m_HitClips[randomClip];

                    //Play it.
                    m_Source.Play();
                }
            }
        }
    }
}