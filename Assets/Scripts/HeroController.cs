using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : PlayerController
{
    public float jumpForce = 100f;

	private Transform groundCheck;
    private bool grounded = false;
    private bool jump = false;
    private Transform pivotHead;
    private readonly List<KeyCode> actions = new List<KeyCode>();



    void Awake()
    {
        base.Awake(); // call inherited method
        groundCheck = transform.Find("GroundDetector");
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
                jump = true;

        if (Input.GetMouseButtonDown(0) && HasTurn)
            weaponController.FireDown();
        else if (Input.GetMouseButtonUp(0) && HasTurn) 
        {
            OnZoomOut();
            weaponController.FireUp();
        }

        UpdateKeyboardAction(KeyCode.LeftArrow);
        UpdateKeyboardAction(KeyCode.RightArrow);
        UpdateKeyboardAction(KeyCode.UpArrow);
        UpdateKeyboardAction(KeyCode.DownArrow);
    }
    void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        if(HasTurn)
            move(horizontal);

        if (actions.Contains(KeyCode.UpArrow))
            rot += 1.0f;

        if (actions.Contains(KeyCode.DownArrow))
            rot -= 1.0f;

        if(HasTurn)
            RotateWeapon();

        if(jump && HasTurn)
        {
            Jump();
        }
    }

    private void UpdateDownAction(KeyCode code)
    {
        if (!actions.Contains(code))
            actions.Add(code);
    }

    private void UpdateUpAction(KeyCode code)
    {
        if (actions.Contains(code))
            actions.Remove(code);
    }

    private void UpdateKeyboardAction(KeyCode code)
    {
        if (Input.GetKeyDown(code))
            UpdateDownAction(code);

        if (Input.GetKeyUp(code))
            UpdateUpAction(code);
    }

    private void Jump()
    {
        animator.SetTrigger("jump");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        jump = false;
    }

    private void move(float horizontal) 
    {
        animator.SetFloat("walk", Mathf.Abs(horizontal));

        if(horizontal < 0 && facingRight)
        {
            facingRight = false;
            Flip();
        } else if(horizontal > 0 && !facingRight)
        {
            facingRight = true;
            Flip();
        }

        Vector3 horizontalVector = new Vector3(horizontal, 0.0f, 0.0f);
        transform.position = transform.position + horizontalVector * Time.deltaTime;
    }
}
