using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{

    public GameObject prefab; // префаб вышки которую хотим построить
    public int cost;//стоимость вышки

    public GameObject upgradedPrefab;//ссылка на префаб апгрейдженой вышки
    public int upgradeCost;//стоимость апгрейда

    public int sellCost;//цена продажи вышки

}
