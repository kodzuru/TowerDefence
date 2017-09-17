using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

    public Text moneyCountdownText; //текст для UI для показа сколько осталось бабла
    
	// Update is called once per frame
	void Update () {
        //покаываем остаток бабла в UI
        moneyCountdownText.text = "$ "+PlayerStats.Money.ToString();
    }
}
