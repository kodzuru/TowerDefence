using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Transform target; //цель пули

    public float speed = 70f;//скорость пули

    public int damage = 50;//стандартный дамаг

    public GameObject impactEffect; //ссылка на частицы из пушки

    public float explosionRadius = 0f; //радиус взрыва пули

    public void Seek(Transform _target)
    {
        target = _target;
    }
	
	// Update is called once per frame
	void Update () {

        //если цели нет
	    if (target == null)
	    {
	        Destroy(gameObject);//уничтожаем объект пули
            return;
	    }
        //получаем направление полёта пули
	    Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //если расстояние до цели <= расстояния пройденого за фрейм 
	    if (direction.magnitude <= distanceThisFrame)
	    {
	        HitTarget();//стреляем по цели
	        return;
	    }

        //перемещаем пулю в направлении цели
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);//пуля всегда смотрит в цель(поворот пули)

    }

    void HitTarget()
    {
        //визуализируем нашы частицы
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation) as GameObject;
        Destroy(effectInstance, 2f);//удаляем объект частиц

        //если разиус взрыва больше нуля, то стреляем массовой плюшкой
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else//если радиус взрыва меньше нуля или ноль, то стреляем таргетной плюшкой
        {
            Damage(target);
        }


        Destroy(gameObject);//удаляем саму пулю
        //Debug.Log("HIT SOMETHIG!");
    }

    void Explode()
    {
        //находим все коллайдеры попадающие в сферу
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        //перебераем все коллайдеры в радиусе
        foreach (Collider collider in colliders)
        {
            //если коллайдер имеет тэг ВРАГ
            if(collider.tag == "Enemy")
                Damage(collider.transform);
        }

    }


    void Damage(Transform _target)
    {
        Enemy e = _target.GetComponent<Enemy>(); //получаем компоненты врага

        if(e != null)
            e.TakeDamage(damage);//наносим дамагу

        //Destroy(_target.gameObject);//удаляем объект цели

    }

    void OnDrawGizmosSelected()//чисто визуальная функция, вспомогательная, ничего не делает
                               //рисуем сферические оси
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
