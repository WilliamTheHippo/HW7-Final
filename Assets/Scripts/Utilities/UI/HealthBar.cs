using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite fullHeart;  // ASSIGN IN INSPECTOR
    public Sprite halfHeart;  // ASSIGN IN INSPECTOR
    public Sprite emptyHeart; // ASSIGN IN INSPECTOR
    
    Player player;
    int previousHealth = 6;
    int currentHealth;
    List<Image> hearts;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        hearts = new List<Image>(this.GetComponentsInChildren<Image>());
        hearts.RemoveAt(0);
    }

    void Update()
    {
        currentHealth = player.GetHealth();
        if (currentHealth == previousHealth) return;
        Debug.Log("Health: " + currentHealth);
        previousHealth = currentHealth;

        foreach (Image h in hearts) {
            if (currentHealth >= 2) {
                h.sprite = fullHeart;
                currentHealth -= 2;
            } else if (currentHealth == 1) {
                h.sprite = halfHeart;
                currentHealth--;
            } else {
                h.sprite = emptyHeart;
            }
        }
    }
}