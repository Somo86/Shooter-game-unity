using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform ExplodeEffect;
    private Transform parentPivot;
    private Transform arrowWrapper;
    private Rigidbody2D rigidbody2D;
    private AudioClip clipExplosion;

    void Awake()
    {
        parentPivot = transform.root.gameObject.transform.Find("R_arm");
        arrowWrapper = transform.Find("ArrowWrapper");
        clipExplosion = Resources.Load<AudioClip>("Sounds/Explosion");
    }

    void Update()
    {
        // Add rotation related to velocity
        // to reproduce arrow real effect
        rigidbody2D = GetComponent<Rigidbody2D>();
        if(rigidbody2D != null)
        {
            var v = rigidbody2D.velocity;
            var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
    }

    public void Explode()
    {
        GetComponent<AudioSource>().PlayOneShot(clipExplosion);
        var explosion = Instantiate(ExplodeEffect, transform.position, transform.rotation, transform);
        explosion.transform.localScale = new Vector3(4.48f, 4.48f, 0);
        Destroy(arrowWrapper.gameObject); //remove arrow and particles
        Destroy(rigidbody2D); //remove rigidBody to avoid gravity on explosion
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {   
        // Avoid exploding if collide itself;
        if(other.gameObject != transform.root.gameObject)
            Explode();
    }
}
