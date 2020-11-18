using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Rigidbody2D Projectile;
    public Transform SpeedBar;
    public float Speed = 10f;
    public float maxSpeed = 200f;

    private float BulletSpeed;
    private Animator animator;
    private Transform root;
    private States state = States.Clean;
    private Transform currentBar = null;
    private PlayerController playerController;
    private AudioClip clipFire;
    private enum States
    {
        Clean,
        Down,
        Up
    }

    void Awake()
    {
        clipFire = Resources.Load<AudioClip>("Sounds/Arrow_Fire");
    }

    void Start()
    {
        BulletSpeed = Speed;
        root = transform.root.gameObject.transform;
        animator = root.Find("animated").gameObject.GetComponent<Animator>();
        playerController = root.GetComponent<PlayerController>();
    }

    void Update()
    {
        // Main state
        if(state == States.Clean)
            BulletSpeed = Speed;
        // Down state
        else if(state == States.Down)
            if(BulletSpeed < maxSpeed){
                BulletSpeed += 0.2f;
                ShowSpeedBar();
            }
            else
                BulletSpeed = maxSpeed;
        // Up state
        else if(state == States.Up)
            Fire();
    }

    private void ShowSpeedBar()
    {
        var maxBarSize = 0.9123467f;

        if(currentBar == null){
            var positionX = playerController.facingRight ? 2f : -2f;
            currentBar = Instantiate(SpeedBar, root.position + new Vector3(positionX,1.05f,0), root.rotation, root);
        }
        var barSprite = currentBar.transform.Find("bar_wrapper").transform.Find("bar");
        var barRenderer = barSprite.GetComponent<SpriteRenderer>();
        var percentage = CalculateSpeedBar();
        barSprite.transform.localScale = new Vector3(percentage * maxBarSize, 0.4338434f, 0);
        if(percentage <= 0.4)
            barRenderer.color = Color.green;
        else if(percentage > 0.4 && percentage <= 0.7)
            barRenderer.color = Color.yellow;
        else
            barRenderer.color = Color.red;
    }

    // Calculate bar percentage
    private float CalculateSpeedBar()
    {
        float percentage = 0;

        if(BulletSpeed >= Speed)
            percentage = BulletSpeed / maxSpeed;

        return percentage;
    }

    public void FireDown()
    {
        state = States.Down;
    }

    public void FireUp()
    {
        state = States.Up;
        Destroy(currentBar.gameObject);
        currentBar = null;
    }

    // Fire arrow. Enemy defines speed via parameters
    // Hero use click down to define velocity
    public void Fire(float definedSpeed = 0)
    {
        animator.SetTrigger("attack");
        GetComponent<AudioSource>().PlayOneShot(clipFire);
        // throw arrow - add distance to not collide himself
        var positionX = playerController.facingRight ? 2f : -2f;
        var securityDistance = new Vector3(positionX, 0, 0);
        // Instanciate arrow
        var bulletInstance = Instantiate(Projectile, transform.position + securityDistance, Projectile.transform.rotation, transform);
        var speed = playerController.facingRight ? BulletSpeed + definedSpeed : -BulletSpeed - definedSpeed;
        bulletInstance.velocity = transform.right.normalized * speed;
        state = States.Clean; // initial state

        Invoke(nameof(FinishTurn), 3);

    }

    private void FinishTurn() 
    {
        //Stop counter back from Game Controller
        playerController.OnStopTime.Invoke();
    }
}
