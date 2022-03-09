using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private int maxHP;

    private int hp;

    private void Start()
    {
        hp = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    public void Damage()
    {
        if (0 < hp)
        {
            hp--;
        }
    }

    public void Heal()
    {
        if (hp < maxHP)
        {
            hp++;
        }
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }
}
