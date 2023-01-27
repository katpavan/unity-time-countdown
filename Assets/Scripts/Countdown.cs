using System; //needed for DateTimeOffset and DateTime
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*

* the timer and button going red doesn't sync up

*/

public class Countdown : MonoBehaviour
{
	public float currentTime = 0f; 
	public float startingTime = 5f;
	public bool startTimer = false;
	public long lastClicked;
	public int affectionScore = 0;
	public int clickCount = 0;
	public Dictionary<int, string> phrases = new Dictionary<int, string>();
	public int lastPhraseKey = 100;

	[SerializeField] TextMeshProUGUI countDownText;
	[SerializeField] TextMeshProUGUI affectionScoreText;
	[SerializeField] TextMeshProUGUI phraseText;
	[SerializeField] GameObject button;


    // Start is called before the first frame update
    void Start()
    {
    	phraseText.gameObject.SetActive(false);

    	button.GetComponent<Image>().color = Color.red;

    	toggleCountdown();
        currentTime = startingTime;

        phrases.Add(0, "you got heart");
        phrases.Add(1, "keep it going!");
        phrases.Add(2, "I believe in you!");
        phrases.Add(3, "you're one of a kind");
        phrases.Add(4, "you're the embodiment of perseverance");
        phrases.Add(5, "defense wins championships");
        phrases.Add(6, "don't stop, don't give up, ever");
    }

    // Update is called once per frame
    void Update()
    {
    	var t = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

    	if (startTimer){
    		currentTime -= 1 * Time.deltaTime;
    		countDownText.text = formatTime(currentTime);
    		
    		if (currentTime <= 1){
    			currentTime = 5;
    			startTimer = false;
    		}
    	}

    	if ((t - lastClicked >= 5) || (clickCount == 0)){
    		button.GetComponent<Image>().color = Color.red;
    	}else{
    		button.GetComponent<Image>().color = Color.yellow;
    	}
    }

    public void bClick(){

    	startTimer = true;
    	toggleCountdown();

    	var t = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

    	//phrase code start
    	phraseText.gameObject.SetActive(true);
    	var x = UnityEngine.Random.Range(0, 7);
    	while(x == lastPhraseKey)
    	{	
    		Debug.Log("hit while loop");
    		x = UnityEngine.Random.Range(0, 7);
    	}
    	print($"{x} ----------- {t}");
    	phraseText.text = phrases[x];
    	//phrase code end

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
