using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text label;

    private const string BestKey = "BestScore";
    private float startZ;
    private int best;

    private void Start()
    {
        startZ = player.position.z;
        best = PlayerPrefs.GetInt(BestKey, 0);
    }

    private void Update()
    {
        int score = Mathf.FloorToInt(player.position.z - startZ);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt(BestKey, best);
        }
        label.text = $"Score: {score}\nBest: {best}";
    }
}