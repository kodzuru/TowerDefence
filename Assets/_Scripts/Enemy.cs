using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{

    public float startSpeed = 10.0f;//начальная скорость врага
    [HideInInspector]
    public float speed;//скорость врага

    public float startHealth = 100; //начальное значение HP вышки
    float health;//текущее значение HP вышки
    public int moneyReward = 25; //награда за уничтожение вышки

    public GameObject destroyEffect; //эффект уничтожения цели

    [Header("Unity Stuff")]
    public Image healthBar;//ссылка на UI элемент картинки healthBar

    bool isDead = false;//враг жив
    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth; //рассчёт в процентах значений healthBar

        if (health <= 0 && !isDead)
            Die();
    }

    void Die()
    {

        isDead = true;//враг мёртв


        PlayerStats.Money += moneyReward;//плюсуем баблишко
        
        //визуалезируем эффект
        GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 2f);

        WaveSpawner.EnemyesAlive--;//уменьшаем переменную к-ва врагов

        Destroy(gameObject);//уничтожаем цель

     }

    public void Slow(float slowFactor)
    {
        speed = startSpeed * (1f - slowFactor);
    }
    
}
