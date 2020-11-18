using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    // Start is called before the first frame update
    public DragonDirection direction;
    public float PositionX;

    void Start()
    {
        if(direction == DragonDirection.Right)
            Flip();
        
        Invoke(nameof(Kill), 60);
    }
    void FixedUpdate() {
        transform.Translate(-(PositionX * (Time.deltaTime / 8)), 0, 0);
    }

    private void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
