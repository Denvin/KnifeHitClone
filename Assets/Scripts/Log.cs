using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [Header("Rotation log")]
    [SerializeField] float speedMin = 2f;
    [SerializeField] float speedMax = 4f;
    [SerializeField] float timeRotate = 2f;
    [SerializeField] float breakTime = 2f;
    [SerializeField] float angularDragNormal = 0.05f;
    [SerializeField] float angularDrag = 0.1f;

    [Header("Coin")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Chance chance;

    [Header("Knife")]
    [SerializeField] GameObject[] knives;
    [SerializeField] int minStartKnife = 1;
    [SerializeField] int maxStartKnife = 3;


    private float speed;
    private int knifeCount;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ActiveCoin();
        ActiveKnives();
        StartCoroutine(RotateLog());
    }

    private void ActiveCoin()
    {
        int rand = Random.Range(0, 100);
        if (rand <= chance.CoinChance)
        {
            coinPrefab.SetActive(true);
            Debug.Log("chance " + rand);
        }
    }

    private void ActiveKnives()
    {
        knifeCount = Random.Range(minStartKnife, maxStartKnife);


        for (int i = 0; i < knifeCount; i++)
        {
            int randKnife = Random.Range(0, knives.Length);

            knives[randKnife].SetActive(true);
        }
    }

    IEnumerator RotateLog()
    {
        while (true)
        {
            speed = Random.Range(-speedMax, speedMax);
            while(speed < speedMin && speed > -speedMin)
            {
                speed = Random.Range(-speedMax, speedMax);
            }

            rb.AddTorque(speed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(timeRotate);
            rb.angularDrag = angularDrag;
            yield return new WaitForSeconds(breakTime);
            rb.angularDrag = angularDragNormal;

        }
    }
    
}
