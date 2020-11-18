using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool facingRight = true;
    public GameObject Weapon;
    public WeaponController weaponController;
    public Transform Health;
    public bool HasTurn;
    public bool IsHero = false;

    public Action OnStopTime;
    public Action<GameObject> OnKill;
    public Action OnLoose;
    public Action OnZoomOut;

    protected Transform animatedGoblin;
    protected Animator animator;
    protected Transform pivotArm;
    protected float rot;

    private Transform weaponHand;
    private Transform weaponArm;

    private HealthBarController healthController;

    protected void Awake()
    {
        animatedGoblin = transform.Find("animated");
        healthController = transform.Find("Health").GetComponent<HealthBarController>();
        animator = animatedGoblin.GetComponent<Animator>();
        pivotArm = transform.Find("R_arm");
        weaponArm = transform.Find("R_arm").transform.Find("lowerarm");
        weaponHand = transform.Find("R_arm").transform.Find("lowerarm").transform.Find("R_hand");
    }

    protected void Start()
    {
        if(!facingRight)
            Flip();
        // Instantiate defined weapon on right hand
        Instantiate(Weapon, weaponHand.position, pivotArm.rotation, weaponArm.transform);
    }

    protected void RotateWeapon()
    {
        rot = Mathf.Clamp(rot, 0, 75); // get value between min and max
        pivotArm.rotation = Quaternion.Euler(0, 0, facingRight? rot : -rot);
    }

    protected void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    public void Kill()
    {
        OnKill.Invoke(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Projectile"){
            healthController.removeLife(25);
        }
    }
}
