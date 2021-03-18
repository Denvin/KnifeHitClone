using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreInfo : MonoBehaviour
{
    #region SingleTon
    public static ScoreInfo Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }
    #endregion
    [Header("Current result")]
    [SerializeField] Text scoreText;
    [SerializeField] Text stageText;

    [Header("Best result")]
    [SerializeField] Text bestScoreText;
    [SerializeField] Text bestStageText;

    [Header("Coin")]
    [SerializeField] Text coinText;


    private int bestScore;
    private int bestStage;
    private int coin;

    public int Coin
    {
        get
        {
            return coin;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        bestScore = PlayerPrefs.GetInt("BestScore");
        bestStage = PlayerPrefs.GetInt("BestStage");
        coin = PlayerPrefs.GetInt("Coin");

        if (bestScoreText != null && bestStageText != null)
        {
            bestScoreText.text = $"SCORE {bestScore}";
            bestStageText.text = $"STAGE {bestStage}";
        }
        else if (scoreText != null && stageText != null)
        {
            scoreText.text = $"{gameManager.Score}";
            stageText.text = $"STAGE {gameManager.Stage}";
        }

        coinText.text = $"{coin}";

        if (gameManager != null)
        {
            Destroy(gameManager.gameObject);
        }
    }

    public void Purchase(int price)
    {
        coin -= price;
        PlayerPrefs.SetInt("Coin", coin);
        coinText.text = $"{coin}";
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        if (bestScoreText != null && bestStageText != null)
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
            bestStage = PlayerPrefs.GetInt("BestStage");
            coin = PlayerPrefs.GetInt("Coin");

            bestScoreText.text = $"SCORE {bestScore}";
            bestStageText.text = $"STAGE {bestStage}";
            coinText.text = $"{coin}";
        }
    }
}
