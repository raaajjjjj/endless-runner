using UnityEngine;

public class FloorFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = player.position.z + 240f;
        transform.position = pos;
    }
}