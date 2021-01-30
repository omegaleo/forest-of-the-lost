using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 velocity;
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition,ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
