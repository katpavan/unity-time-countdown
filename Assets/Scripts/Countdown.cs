using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*

1. show a button, click on it, store the timestamp of when it was clicked on
2. make a heart score go up and display it whenever the button is clicked
3. clicking on the button starts the timer
4. you can only get points when the timer is done
5. display a random message when you click the button 
6. don't display the same message after clicking the button

*/

public class Countdown : MonoBehaviour
{
	public float currentTime = 0f; 
	public float startingTime = 7200f;
	public bool showTimer = true;

	[SerializeField] TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        // print(currentTime);
        countDownText.text = formatTime(currentTime);

        if (currentTime <= 0){
        	currentTime = 0;
        	showTimer = false;
        }else{
        	showTimer = true;
        }

        if (!showTimer) {
        	countDownText.gameObject.SetActive(false);
        }else{
        	countDownText.gameObject.SetActive(true);
        }
    }

    public string formatTime(float ct)
    {
    	int hours = (int) ct / 3600;
    	int minutes = (int) ct / 60; 
    	minutes = minutes > 60 ? minutes - hours * 60 : minutes;
    	int seconds = (int) ct % 60;

    	var s = $"{hours}:{minutes}:{seconds}";
    	return s;
    }
}
