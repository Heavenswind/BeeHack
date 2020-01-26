using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public GameObject bullet;
    public GameObject bullet_emitter;
    Rigidbody rb;
    float bullet_force = 100.0f;
    public float speed = 50.0f;
    float maxDist = 20.0f;
    float timeSinceLastShot = 1.0f;
    float rateOfFire = 3.0f;
    public int health = 20;
    public GameObject bar; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        timeSinceLastShot += Time.deltaTime;
    }

    void Chase()
    {
            transform.LookAt(player.transform);
            rb.velocity = transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) <= maxDist && timeSinceLastShot >= rateOfFire)
            {
                //Here Call any function U want Like Shoot at here or something
                //Reset variables
                timeSinceLastShot = 0;

                //The Bullet instantiation happens here.
                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(bullet, bullet_emitter.transform.position, bullet_emitter.transform.rotation) as GameObject;

                //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
                //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

                //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

                //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
                Temporary_RigidBody.AddForce(transform.forward * bullet_force);

                //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
                Destroy(Temporary_Bullet_Handler, 4.0f);
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            health -= 5;
            Destroy(other.gameObject);
            bar.transform.localScale = new Vector3((health / 20.0f), 1, 1);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (other.tag == "Player")
        {
            //Infect him
        }
    }
}
