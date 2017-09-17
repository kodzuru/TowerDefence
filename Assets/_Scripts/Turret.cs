using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    Transform target; //положение цели(определяем саму цель)
    Enemy targetEnemy;//переменная врага


    [Header("General")]
    public float range = 15f; // расстояние в котором мы ищем цель

    [Header("Use Bullets(default)")]
    public float fireRate = 1f; //скорость атаки
    float fireCountdown = 0f; // задержка между выстрелами
    public GameObject bulletPrefab; //объект пули

    [Header("Use Laser")]
    public bool useLaser = false;//используем ли лазер?
    public float slowAmount = 0.5f;// процентное замедление врага
    public int damageOverTime = 30;//дамаг пер тайм

    public LineRenderer lineRenderer;//ссылка на лайн рендер лазера
    public ParticleSystem impactEffect;//частицы начала стрельбы из лазера
    public Light impactLight;//эффект света при попадании в объект

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy"; //тэг с которым связывается объект
    public Transform partToRotate; // ссылка на объект короый будем вращать
    public float turnSpeed = 10f; //скорость поворота вышки

    public Transform firePoint; //цель стрельбы



    // Use this for initialization
    void Start () {
        //вызываем метод по поиску цели в рендже
		InvokeRepeating("UpdateTarget", 0f, 0.1f);
	}

    void UpdateTarget()
    {
        //находим врагов по тэгу и заносим их в массив геймобъектов
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;// начальная ближайшая дистанция
        GameObject nearestEnemy = null;// пустая ссылка на ближайший объект

        //перебираем все цели
        foreach (GameObject enemy in enemies)
        {
            //находим расстояние между нами и целью
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //если дистанция до врага меньше наименьшей дистанции
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy; //получаем ближайшего врага
            }
        }

        //если нашли врага и наименьшая дистанция меньше ренжа
        if (nearestEnemy != null && shortestDistance <= range)
        {
            //получаем позицию врага
            target = nearestEnemy.transform;

            targetEnemy = nearestEnemy.GetComponent<Enemy>();//передаём в переменную врага объект врага
        }
        else//если враг покидает рендж
        {
            //удаляем цель с врага
            target = null;
        }


    }
	
	// Update is called once per frame
	void Update () {
        //если цель уничтожена или цели нет
	    if (target == null)
	    {
            //если используем залер
	        if (useLaser)
	        {
                //если лазер включён и цель уничтожена или её нет
	            if (lineRenderer.enabled)
	            {
                    lineRenderer.enabled = false;//выключаем луч
                    impactEffect.Stop();//останавливается эффект частиц
	                impactLight.enabled = false;//выключаем свет
	            }
            }
	        return;
	    }
            

        //закрепляем вышку за целью
	    LockOnTarget();
        //если используем лазер
	    if (useLaser)
	    {
	        Laser();
	    }
	    else//иначе обычную пулю
	    {
            //если задержка дошла до нуля
            if (fireCountdown <= 0)
            {
                //стреляем
                Shoot();
                fireCountdown = 1f / fireRate;//устанавливаем задержку не равной нулю
            }
            fireCountdown -= Time.deltaTime; // уменьшаем задержку
        }

        

	}

    void Laser()
    {
        //дамажим врага пер тайм
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);//заедляем врага

        //если луч выключен, включаем его
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;//появляется луч
            impactEffect.Play();//запускается эффект частиц
            impactLight.enabled = true;//включаем свет
        }

        lineRenderer.SetPosition(0, firePoint.position);//начала луча
        lineRenderer.SetPosition(1, target.position);//конец луча

        Vector3 dir = firePoint.position - target.position;//получаем направление(точку) цели

        impactEffect.transform.position = target.position + dir.normalized;//где появляются частицы на враге

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);//поворот частиц к цели

    }

    void LockOnTarget()
    {
        /*  закрепляем вышку за целью  */
        //определяем направление между врагом и вышкой
        Vector3 direction = target.position - transform.position;
        //определяем углы относительно вышки
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //высчитываем на сколько надо повернуть вышку и сглаживаем вычисления
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        //утснавливаем углы в позицию вышки
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Shoot()
    {
        //создаём объект пули
        GameObject bulletGO =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
        //создаём объект класса Bullet
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        //если объект создался
        if (bullet != null)
            bullet.Seek(target);//вызываем публичный метод преследования объекта из класса Bullet
    }

    void OnDrawGizmosSelected()//чисто визуальная функция, вспомогательная, ничего не делает
        //рисуем сферические оси
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
