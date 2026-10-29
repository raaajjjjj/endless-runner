using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float laneChangeSpeed = 15f;
    [SerializeField] private float jumpVelocity = 11f;
    [SerializeField] private float gravity = 30f;

    private const float GroundY = 0.5f;

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private float verticalVelocity;

    private bool IsGrounded => transform.position.y <= GroundY + 0.001f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(0, currentLane - 1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(2, currentLane + 1);

        if (IsGrounded && (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            verticalVelocity = jumpVelocity;

        verticalVelocity -= gravity * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, (currentLane - 1) * laneWidth,
                                  laneChangeSpeed * Time.deltaTime);
        pos.y += verticalVelocity * Time.deltaTime;
        pos.z += forwardSpeed * Time.deltaTime;

        if (pos.y <= GroundY)
        {
            pos.y = GroundY;
            verticalVelocity = 0f;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>() != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}