using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float health;
    Rigidbody rb;
    float timeSinceLastShot;
    public float rateOfFire;
    public float speed;

    [Header("Bullet Variables")]
    public GameObject Bullet;
    public GameObject Bullet_Spawn;
    public float bulletSpeed;
    public float Bullet_Forward_Force;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        health = 100.0f;
        speed = 200.0f;
        rateOfFire = 1.0f;

        Bullet_Forward_Force = 3000.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        float y = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, y);
        rb.velocity = movement * speed * Time.deltaTime;

        if (movement != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movement);

        Shoot();
    }

    void Shoot()
    {
        if (CrossPlatformInputManager.GetButton("Jump") && timeSinceLastShot >= rateOfFire)
        {
            //Reset variables
            timeSinceLastShot = 0;

            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Spawn.transform.position, Bullet_Spawn.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 4.0f);
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            health -= 5;
            ///bar.transform.localScale = new Vector3(health / 100.0f, 1, 1);
        }
        else if (other.tag == "HealthPack")
        {
            health += 20;
            other.transform.LookAt(gameObject.transform);
            //bar.transform.localScale = new Vector3(health / 100.0f, 1, 1);
            Destroy(other.transform.parent.gameObject);
        }
    }
}
