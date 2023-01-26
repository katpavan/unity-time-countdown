using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*

2*60*60 = 7200 is two hours

7000 % 3600 = 3400

200 seconds = 3 minutes 20 seconds

so it should show this for 7000 seconds
1:56:40



*/

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
    	countDownText.gameObject.SetActive(false);
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
        countDownText.text = currentTime.ToString(timeFormats[format]); //shows whole numbers only

        if (currentTime <= 0){
        	currentTime = 0;
        	showTimer = false;
        }

        if (!showTimer) {
        	countDownText.gameObject.SetActive(false);
        }else{
        	countDownText.gameObject.SetActive(true);
        }
    }

    public enum TimerFormats{
    	Whole,
    	TenthDecimal,
    	HundrethsDecimal
    }
}
