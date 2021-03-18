using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    private SpriteRenderer skin;
    private void Awake()
    {
        skin = GetComponent<SpriteRenderer>();
        UpdateSkin();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateSkin()
    {
        int index = PlayerPrefs.GetInt("PlayerSkin");
        skin.sprite = sprites[index];
    }
}
