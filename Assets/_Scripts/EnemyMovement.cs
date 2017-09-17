using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {


    private Transform target; //цель
    private int waypointsIndex = 0; //начальный индекс массива пунктов назначения

    Enemy enemy;//ссылка на объект врага(на его скрипт) 


    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[waypointsIndex];//первая цель(первый пункт назначения)
    }

    void Update()
    {
        //вектор направления
        Vector3 direction = target.position - transform.position;
        //применяем к объекту направление куда и с какой скоростью он должен двигаться
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        //если расстояние от объекта до новой позиции меньше ЗНАЧЕНИЯ
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GerNextWaypoint();//получить следующую точку позиции
        }

        enemy.speed = enemy.startSpeed; //возвращаем прежнюю скорость(скорость будет прежняя если цель покинет радиус поражения)
    }


    void GerNextWaypoint()
    {
        //если индекс меньше длины массива позиций
        if (waypointsIndex < Waypoints.points.Length - 1)
            target = Waypoints.points[++waypointsIndex]; //получаем новую позицию
        else //если вышли за предел массива точек следования, то объект достиг цели и удаляем объект
        {
            EndPath();//по достижению врага конца пути
        }
    }

    void EndPath()
    {
        PlayerStats.Lives--;//уменьшаем к-во жизней
        WaveSpawner.EnemyesAlive--;//уменьшаем переменную к-ва врагов
        Destroy(gameObject);//уничтожаем объект

    }


}
