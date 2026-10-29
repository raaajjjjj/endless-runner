using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float laneChangeSpeed = 15f;

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(0, currentLane - 1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(2, currentLane + 1);

        float targetX = (currentLane - 1) * laneWidth;
        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, targetX, laneChangeSpeed * Time.deltaTime);
        pos.z += forwardSpeed * Time.deltaTime;
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger with: {other.name}");
        if (other.GetComponent<Obstacle>() != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}