using System; //needed for DateTimeOffset and DateTime
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*

* the timer and button going green doesn't sync up

*/

public class Countdown : MonoBehaviour
{
	public float timerCurrent = 0f; 
	public float interval = 5f;
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

    	button.GetComponent<Image>().color = Color.green;

    	toggleCountdown();
        timerCurrent = interval;

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
    		timerCurrent -= 1 * Time.deltaTime;
    		countDownText.text = formatTime(timerCurrent);
    		
    		if (timerCurrent <= 1){
    			timerCurrent = 5;
    			startTimer = false;
    		}
    	}

    	if ((t - lastClicked >= 5) || (clickCount == 0)){
    		button.GetComponent<Image>().color = Color.green;
    	}else{
    		button.GetComponent<Image>().color = Color.white;
    	}
    }

    public void bClick(){

    	var t = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

    	//phrase code start
    	phraseText.gameObject.SetActive(true);

    	//phrase code end

    	var y = (t - lastClicked >= 5) || (clickCount == 0);
    	var yx = y && (startTimer == false);

    	if (yx){
    		startTimer = true;
    		toggleCountdown();

            var x = generatePhraseIndex();
            phraseText.text = phrases[x];

    		clickCount += 1;
    		affectionScore += 10;
            
    		lastClicked = t;
    		affectionScoreText.text = affectionScore.ToString();
    		// Debug.Log(lastClicked);
    	}

    	
    }

    public int generatePhraseIndex(){
        var x = UnityEngine.Random.Range(0, 7);
        while(x == lastPhraseKey)
        {   
            x = UnityEngine.Random.Range(0, 7);
        }
        return x;
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

    	//could get minutes hours like this too
    	// var differenceInHours = (DateTimeOffset.Now - lastRewardedChatDate).TotalHours;
    	// var differenceInMinutes = (DateTimeOffset.Now - lastRewardedChatDate).Minutes;

    	var s = $"{hours}:{minutess}:{secondss}";
    	return s;
    }
}
