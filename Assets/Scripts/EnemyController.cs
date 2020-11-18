using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlayerController
{
    private GameObject Hero;
    private float dist;
    private float shootingAngle = -1;
    private enum Position
    {
        Right,
        Left
    }
    private enum States
    {
        Initial,
        Rotate,
        Shoot,
        EndTurn
    }
    private States currentState = States.Initial;

    void Awake()
    {
        Hero = GameObject.FindGameObjectsWithTag("Hero")[0];
        dist = Vector3.Distance(Hero.transform.position, transform.position);
        base.Awake();
    }

    void Start()
    {
        // Face the enemy in relation to the Hero position
        Face();
        base.Start(); 
    }

    void Update()
    {
        dist = Vector3.Distance(Hero.transform.position, transform.position);
    }

    void FixedUpdate()
    {
        // check on each frame enemy is properly faced
        Face();

        if(HasTurn) 
        {
            if(shootingAngle == -1) {
                shootingAngle = dist;
                currentState = States.Rotate;
            }
            if(currentState == States.Rotate)
                RotateToAngle();
            else if(currentState == States.Shoot)
                Shoot();
            else if(currentState == States.EndTurn)
                Invoke(nameof(Reset), 2.0f);    
        }
    }

    private void Shoot()
    {
        OnZoomOut();
        weaponController.Fire(CalculateVelocity());
        currentState = States.EndTurn;
    }

    private float CalculateVelocity()
    {
        var dir = Hero.transform.position - transform.position;
        var h = dir.y;
        dir.y = 0;
        var dist = dir.magnitude;
        var a = shootingAngle * Mathf.Deg2Rad;

        dist += h / Mathf.Tan(a);

        return Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a)) * Random.Range(1.2f, 1.8f);
    }

    // Rotate the hand until gets the desired angle
    // The angle is in relatin with distance between elements
    private void RotateToAngle()
    {
        if(rot < shootingAngle)
        {
            rot += 1.0f;
            RotateWeapon();
        } else {            
            currentState = States.Shoot;
        }
        
    }

    // Back rotation to initial state
    private void Reset()
    {
        shootingAngle = -1;
        rot = 0;
    }

    private void Face()
    {
        if(RelativePosition() == Position.Right)
            facingRight = true;
    }

    private Position RelativePosition() {
        var HeroPositionX = Hero.transform.position.x;
        var CurrentPositionX = transform.position.x;
        return HeroPositionX <= CurrentPositionX ? Position.Left : Position.Right; 
    }
}
