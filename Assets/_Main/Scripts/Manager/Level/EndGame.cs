using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private string WINNING_TEXT = "You Win!";
    [SerializeField]
    private string LOSING_TEXT = "You Lose!";

    [SerializeField]
    private GameState GameState;
    [SerializeField]
    private TextMeshProUGUI EndGameText;

    private void Start()
    {
        EndGameText.text = GameState.IsWinning ? WINNING_TEXT : LOSING_TEXT;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            if (sceneIndex >= 0)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}