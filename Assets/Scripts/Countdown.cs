using System; //needed for DateTimeOffset and DateTime
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*

5. display a random message when you click the button 
6. don't display the same message after clicking the button

*/

public class Countdown : MonoBehaviour
{
	public float currentTime = 0f; 
	public float startingTime = 5f;
	public bool startTimer = false;
	public long lastClicked;
	public int affectionScore = 0;
	public int clickCount = 0;

	[SerializeField] TextMeshProUGUI countDownText;
	[SerializeField] TextMeshProUGUI affectionScoreText;

    // Start is called before the first frame update
    void Start()
    {
    	toggleCountdown();
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {

    	if (startTimer){
    		currentTime -= 1 * Time.deltaTime;
    		countDownText.text = formatTime(currentTime);

    		if (currentTime <= 0){
    			currentTime = 5;
    		}
    	}
    }

    public void bClick(){

    	startTimer = true;
    	toggleCountdown();

    	var t = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

    	if (lastClicked == 0){
    		lastClicked = t;
    	}

    	if ((t - lastClicked >= 5) || (clickCount == 0)){
    		clickCount += 1;
    		affectionScore += 2;
    		lastClicked = t;
    		affectionScoreText.text = affectionScore.ToString();
    		Debug.Log(lastClicked);
    	}

    	
    }

    public void toggleCountdown(){
    	if (!startTimer) {
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
    	var minutess = minutes < 10 ? $"0{minutes}" : minutes.ToString();
    	int seconds = (int) ct % 60;
    	var secondss = seconds < 10 ? $"0{seconds}" : seconds.ToString();

    	var s = $"{hours}:{minutess}:{secondss}";
    	return s;
    }
}
