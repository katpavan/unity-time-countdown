using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
	public float currentTime = 0f; 
	public float startingTime = 7200f;
	public bool showTimer = true;
	public TimerFormats format;

	[SerializeField] TextMeshProUGUI countDownText;
	private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    // Start is called before the first frame update
    void Start()
    {
    	// countDownText.gameObject.SetActive(false);
    	format = TimerFormats.TenthDecimal;
        currentTime = startingTime;
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
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

    public enum TimerFormats{
    	Whole,
    	TenthDecimal,
    	HundrethsDecimal
    }
}
