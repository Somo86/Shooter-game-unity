using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    // Start is called before the first frame update
    public float initialLife = 100;
    private Transform bar;
    private float maxLife = 100;
    private float currentLife;
    private float maxBarLength;
    private Animator animator;
    private PlayerController rootController;

    void Awake()
    {
        bar = transform.Find("health_bar");
        animator = transform.root.transform.Find("animated").GetComponent<Animator>();
        rootController = transform.root.GetComponent<PlayerController>();

        if(initialLife > maxLife)
            maxLife = initialLife;
        
        currentLife = initialLife;
        maxBarLength = bar.transform.localScale.x;
    }

    void Update(){
        bar.transform.localScale = new Vector3(
            calulateBarLength(), 
            bar.transform.localScale.y, 
            bar.transform.localScale.z
        );
    }

    private float calulateBarLength()
    {
        return maxBarLength * currentLife / maxLife;
    }

    public void addLife(float value)
    {
        var newLife = currentLife + value;
        if(newLife >= maxLife)
            currentLife = maxLife;
        else
            currentLife = newLife;
    }

    public void removeLife(float value)
    {
        var newLife = currentLife - value;
        if(newLife <= 0)
        {
            animator.SetTrigger("death");
            rootController.Kill();
            
            if(rootController.IsHero)
                rootController.OnLoose();
        }      
        else
           currentLife = newLife; 
    }
}
