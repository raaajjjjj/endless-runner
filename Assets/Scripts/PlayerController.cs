using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float laneChangeSpeed = 15f;
    [SerializeField] private float jumpVelocity = 13f;
    [SerializeField] private float gravity = 30f;
    [SerializeField] private float slideDuration = 0.6f;

    private const float StandHeight = 1f;
    private const float SlideHeight = 0.5f;

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private float verticalVelocity;
    private float slideTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(0, currentLane - 1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(2, currentLane + 1);

        bool grounded = transform.position.y <= transform.localScale.y * 0.5f + 0.001f;

        if (grounded && (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            verticalVelocity = jumpVelocity;
            slideTimer = 0f;
        }
        else if (grounded && (Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            slideTimer = slideDuration;
        }

        slideTimer = Mathf.Max(0f, slideTimer - Time.deltaTime);
        float height = slideTimer > 0f ? SlideHeight : StandHeight;
        transform.localScale = new Vector3(1f, height, 1f);
        float groundY = height * 0.5f;

        verticalVelocity -= gravity * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y = Mathf.Max(pos.y, groundY);
        pos.x = Mathf.MoveTowards(pos.x, (currentLane - 1) * laneWidth,
                                  laneChangeSpeed * Time.deltaTime);
        pos.y += verticalVelocity * Time.deltaTime;
        pos.z += forwardSpeed * Time.deltaTime;

        if (pos.y <= groundY)
        {
            pos.y = groundY;
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