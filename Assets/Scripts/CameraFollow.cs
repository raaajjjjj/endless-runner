using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float height = 4f;
    [SerializeField] private float distanceBehind = 6f;

    private void LateUpdate()
    {
        transform.position = new Vector3(
            target.position.x, height, target.position.z - distanceBehind);
    }
}