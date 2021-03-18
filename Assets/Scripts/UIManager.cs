using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class UIManager : MonoBehaviour
{
    #region SingleTon

    public static UIManager Instance { get; private set; }



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

    [SerializeField] GameObject knivesPanel;
    [SerializeField] GameObject iconKnifePrefab;
    [SerializeField] Color activeIconColor;
    [SerializeField] Color deactiveIconColor;
    [SerializeField] Text scoreText;
    [SerializeField] Text stageText;
    [SerializeField] Text coinText;

    private int indexIconKnife = 0;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnScore += ChangeScore;
        gameManager.OnNextStage += ChangeStage;
        gameManager.OnCoin += ChangeCoin;
        ChangeScore();
        ChangeStage();
        ChangeCoin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateIconKnife(int count)
    {
        indexIconKnife = 0;
        Debug.Log("Count knives = " + count);
        for (int i = 0; i < count; i++)
        {
            LeanPool.Spawn(iconKnifePrefab, knivesPanel.transform);
            knivesPanel.transform.GetChild(indexIconKnife++).GetComponent<Image>().color = activeIconColor;
        }
        indexIconKnife = 0;
    }

    public void DeactiveIconKnife()
    {
        knivesPanel.transform.GetChild(indexIconKnife++).GetComponent<Image>().color = deactiveIconColor;
    }
    private void ChangeStage()
    {
        stageText.text = $"STAGE {gameManager.Stage}";
    }

    private void ChangeCoin()
    {
        coinText.text = $"{gameManager.Coin}";
    }

    private void ChangeScore()
    {
        scoreText.text = $"{gameManager.Score}";
    }
}
