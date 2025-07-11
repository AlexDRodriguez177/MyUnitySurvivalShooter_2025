using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int score;
    public static int highScore;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        Instance = this;
        text1 = GetComponent<TextMeshProUGUI>();
        score = 0;
       

    }

    public void SaveGameState()
    {
// save variable 
            PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void LoadGameState()
    {
// load variable 
            if(PlayerPrefs.HasKey("HighScore"))
            {
              score = PlayerPrefs.GetInt("HighScore");
            }
            else
            {
            highScore = 0;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
    }

    public void ShowScore()
    {
        text1.text = "Score" + score;
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
            SaveGameState();
        }
    }
    void Update()
    {
        text1.text = "Score: " + score; 
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SaveGameState();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {

            LoadGameState();
        }
    }
}
