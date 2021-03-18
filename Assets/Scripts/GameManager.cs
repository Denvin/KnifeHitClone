using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lean.Pool;

public class GameManager : MonoBehaviour
{
    public Action OnEndStage = delegate { };
    public Action OnNextStage = delegate { };
    public Action OnScore = delegate { };
    public Action OnCoin = delegate { };

    [Header("Log")]
    [SerializeField] Transform logPosition;
    [SerializeField] GameObject logPrefab;
    [SerializeField] GameObject hitLogFX;
    [SerializeField] GameObject destroyLogFX;
    [SerializeField] float timeFX = 2f;
    [SerializeField] AudioClip destroyLogClip;

    [Header("Knife")]
    [SerializeField] Transform knifePosition;
    [SerializeField] GameObject knifePrefab;
    [SerializeField] int knifeCounMin = 4;
    [SerializeField] int knifeCountMax = 8;

    [SerializeField] float timeNextStage = 2f;


    private int knifeCount;
    private int knifeCountStart;
    private int score = 0;
    private int bestScore;
    private int stage = 1;
    private int bestStage;
    private int coin;
    private bool isGameOver = false;


    private GameObject knife;
    private GameObject log;
    private GameObject logFX;
 
    public Transform KnifePosition
    {
        get
        {
            return knifePosition;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    public int BestScore
    {
        get
        {
            return bestScore;
        }
    }
    public int Stage
    {
        get
        {
            return stage;
        }
    }
    public int BestStage
    {
        get
        {
            return bestStage;
        }
    }

    public int Coin
    {
        get
        {
            return coin;
        }
    }

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("Coin");
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCountKnife();
        UIManager.Instance.CreateIconKnife(knifeCount);
        
        SpawnLog();
        SpawnKnife();
        score = 0;
    }


    public void CheckCountKnife()
    {
        score++;
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        OnScore();

        if (knifeCount > 0)
        {
            SpawnKnife();
            Debug.Log(knifeCount);
        }
        else
        {
            stage++;
            if (stage > bestStage)
            {
                bestStage = stage;
                PlayerPrefs.SetInt("BestStage", bestStage);
            }
            
            StartCoroutine(NextStageCoroutine());            
        }
    }

    public void HitLog()
    {
        if (knifeCount <= 0)
        {
            Instantiate(destroyLogFX, logPosition.position, Quaternion.identity);
        }
        else
        {
            Instantiate(hitLogFX, logPosition.position, Quaternion.identity);

        }
    }

    public void AddCoin()
    {
        coin++;
        OnCoin();
        PlayerPrefs.SetInt("Coin", coin);

    }

    private void StartCountKnife()
    {
        knifeCountStart = UnityEngine.Random.Range(knifeCounMin, knifeCountMax);
        knifeCount = knifeCountStart;
        Debug.Log("All knives " + knifeCountStart);
    }

    private void SpawnKnife()
    {
        knifeCount--;
        knife = LeanPool.Spawn(knifePrefab, knifePosition);        
    }

    private void SpawnLog()
    {
        log = Instantiate(logPrefab, logPosition.position, Quaternion.identity);
    }
    
    IEnumerator NextStageCoroutine()
    {
        OnEndStage();
        //logFX = Instantiate(destroyLogFX, logPosition.position, Quaternion.identity);
        AudioManager.Instance.PlaySound(destroyLogClip);
        Destroy(log);
        yield return new WaitForSeconds(timeNextStage);
        //Destroy(logFX);
        LeanPool.DespawnAll();
        SpawnLog();
        StartCountKnife();
        UIManager.Instance.CreateIconKnife(knifeCount);
        SpawnKnife();
        OnNextStage();


    }

}
