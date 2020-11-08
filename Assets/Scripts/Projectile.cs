using System.Collections;
using System.Collections.Generic;
using Resources.Score;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int stress;
    [SerializeField] private int time;
    [SerializeField] private bool shootToDestroy;
    [SerializeField] private bool collectible;
    [SerializeField] private bool badToSkip;
      
        
    [SerializeField] private PlayerController player;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();
 
    }

    public void ReceiveDamage(int damage)
    {
        if(shootToDestroy) hp -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && collectible)
            Taken();
    }

    private void Update()
    {
        if (hp <= 0) Die();
    }

    private void Die()
    {
        player.changeTime(time);
        player.changeStress(stress);
        
        Destroy(gameObject);
    }

    private void Taken()
    {
        player.changeTime(time);
        player.changeStress(stress*5);
        
        Destroy(gameObject);
    }

    public void Missed()
    {
        if(badToSkip)
            player.changeStress(stress*3);
        
        Destroy(gameObject);
    }
    
    
    
    
    
   
}
