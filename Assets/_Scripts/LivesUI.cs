using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    public Text livesText;//место для отображение текста



	// Update is called once per frame
	void Update () {
        livesText.text = PlayerStats.Lives.ToString() + " LIVES";
	}
}
