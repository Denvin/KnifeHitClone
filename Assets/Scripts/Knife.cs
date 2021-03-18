using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] float throwForce = 20f;

    [Header("FX")]
    [SerializeField] GameObject hitFX;
    [SerializeField] float timeFX = 1f;

    [Header("Knife Fall")]
    [SerializeField] float forceFallMin = -10f;
    [SerializeField] float forceFallMax = 10f;
    [SerializeField] float forceFallY = -30f;
    [SerializeField] float angularVelocityMin = 20f;
    [SerializeField] float angularVelocityMax = 50f;

    [Header("Audio")]
    [SerializeField] AudioClip flyKnife;
    [SerializeField] AudioClip knifeToLog;
    [SerializeField] AudioClip knifeToKnife;
    [SerializeField] AudioClip knifeToCoin;

    private bool isActive = true;

    private GameObject hitEffect;
    private Rigidbody2D rb;
    private GameManager gameManager;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

        gameManager.OnEndStage += KnifeFall;
        gameManager.OnNextStage += ActivationKnife;
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlaySound(flyKnife);
                rb.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
                UIManager.Instance.DeactiveIconKnife();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            return;
        }

        if (collision.gameObject.tag == "Coin")
        {
            AudioManager.Instance.PlaySound(knifeToCoin);
            gameManager.AddCoin();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Log")
        {
            AudioManager.Instance.PlaySound(knifeToLog);
            gameManager.HitLog();
            hitEffect = Instantiate(hitFX, transform.position, Quaternion.identity);
            //Destroy(hitEffect, timeFX);
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            this.transform.parent = collision.transform;
            isActive = false;
            gameManager.CheckCountKnife();
        }
        else if (collision.gameObject.tag == "Knife")
        {
            isActive = false;
            AudioManager.Instance.PlaySound(knifeToKnife);
            rb.velocity = Vector2.down * throwForce / 2;
            rb.angularVelocity = Random.Range(angularVelocityMin, angularVelocityMax);
            ScenesLoader.Instance.LoadNextScene();
        }
    }

    private void KnifeFall()
    {
        //this.transform.parent = gameManager.KnifePosition;
        this.transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.angularVelocity = Random.Range(angularVelocityMin, angularVelocityMax);
        rb.AddForce(new Vector2(Random.Range(forceFallMin, forceFallMax), forceFallY), ForceMode2D.Impulse);

    }
    private void ActivationKnife()
    {
        isActive = true;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!isActive)
    //    {
    //        return;
    //    }

    //    if (collision.gameObject.tag == "Log")
    //    {
    //        Debug.Log("I'm here!");
    //        rb.bodyType = RigidbodyType2D.Kinematic;
    //        rb.velocity = Vector2.zero;
    //        this.transform.SetParent(collision.collider.transform);
    //        isActive = false;
    //    }
    //}


}
