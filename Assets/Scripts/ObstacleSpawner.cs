using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnDistanceAhead = 60f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float lifetime = 15f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < spawnInterval) return;
        timer = 0f;

        int lane = Random.Range(0, 3);
        Vector3 pos = new Vector3(
            (lane - 1) * laneWidth, 0.75f, player.position.z + spawnDistanceAhead);
        GameObject obstacle = Instantiate(obstaclePrefab, pos, Quaternion.identity);
        Destroy(obstacle, lifetime);
    }
}