using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    private Vector3 offset;
    public bool followTarget = false;

    private Camera camera;
    private float originalSize;

    void Start()
    {
        camera = GetComponent<Camera>();
        originalSize = camera.orthographicSize;
        offset = Target.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        if(followTarget)
            Follow();
    }

    public void MoveCameraTo(Transform destination)
    {
        camera.orthographicSize = originalSize;
        transform.position = new Vector3(destination.position.x, destination.position.y, transform.position.z);
    }

    // Camera follow X axis
    private void Follow()
    {
        var cameraPosition = Target.position - offset;
        transform.position = new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z);
    }

    public void ZoomOut()
    {
        var newSize = originalSize * 2f;
        camera.orthographicSize = newSize;
    }
}
