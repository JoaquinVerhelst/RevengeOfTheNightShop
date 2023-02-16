using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.ParticleSystem;

public class ThrowableObjectCollision : MonoBehaviour
{
    GameObject player;
    public bool isHit;
    ParticleSystem particles;
    [SerializeField] GameObject thisObject;
    [SerializeField] List<GameObject> ThrowableObjects = new List<GameObject>();

    public AudioClip m_BottleHit;
    public AudioClip m_BottleBreak;

    Vector3 targetTransform;
    float gravity = 9.81f;
    float firingAngle = 45f;

    new BoxCollider collider;
    public bool isThrown = false;

    int index;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        index = Random.Range(0, ThrowableObjects.Count);
        Instantiate(ThrowableObjects[index], thisObject.transform);
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }

    private void Update()
    {
        if (isThrown)
        {
            transform.parent = null;
            collider.enabled = true;
            isThrown = false;
            StartCoroutine(SimulateProjectile());
        }
    }

    public void Initialize(Vector3 target, float angle )
    {
        firingAngle= angle;
        targetTransform = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Bottle"))
        //{
        //    Debug.Log("bottle Hit");
        //    player = other.gameObject;

        //    thisObject.SetActive(false);
        //    particles.Play();
        //}
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            player = other.gameObject;

            ThrowableHit();
        }
        else if (other.CompareTag("World"))
        {
            //AudioSource.PlayClipAtPoint(m_BottleBreak, this.gameObject.transform.position);
            thisObject.SetActive(false);
            particles.Play();
        }


    }

    private void ThrowableHit()
    {
        if (player == null) return;

        PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();

        if (playerLogic == null) return;
        collider.enabled = false;
        playerLogic.TakeDamage();
        thisObject.SetActive(false);
        //AudioSource.PlayClipAtPoint(m_BottleBreak, this.gameObject.transform.position);
        particles.Play();
    }



    IEnumerator SimulateProjectile()
    {
        // Move projectile to the position of throwing object + add some offset if needed.
        //throwableObject.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);



        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, targetTransform);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = (target_Distance / Vx);

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(targetTransform - transform.position);

        float elapse_time = 0;


        while (elapse_time < flightDuration)
        {

            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }

}
