using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragonDirection {
    Left,
    Right
}
public class DragonCreator : MonoBehaviour
{
    public GameObject Dragon;
    private Transform AppearRight;
    private Transform AppearLeft;
    void Awake()
    {
        AppearRight = GameObject.Find("StartPointRight").transform;
        AppearLeft = GameObject.Find("StartPointLeft").transform;
    }

    void Start()
    {
        InvokeRepeating(nameof(CreateDragon), 0, Random.Range(5f, 15f));
    }

    private void CreateDragon()
    {
        var random = Random.Range(0, 2);
        var appearPoint = random == 0 ? AppearRight : AppearLeft;
        var dragon = Instantiate(Dragon, appearPoint.position, appearPoint.rotation);
        var controller = dragon.GetComponent<DragonController>();
        controller.direction = random == 0 ? DragonDirection.Left : DragonDirection.Right;
        controller.PositionX = appearPoint.position.x;
    }
}
