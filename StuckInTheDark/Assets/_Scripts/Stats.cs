using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour {

	private Canvas end;
	private Text currStats, bestStats, msg;

	private Color victory, defeat;

	private float endT;
	private int endP, endB;

	private Flashlight player;

	// Use this for initialization
	void Start () {
		end = GetComponent<Canvas> ();
		msg = GameObject.FindGameObjectWithTag ("endText").GetComponent<Text>();
		currStats = GameObject.FindGameObjectWithTag ("currentRun").GetComponent<Text>();
		bestStats = GameObject.FindGameObjectWithTag ("hiscores").GetComponent<Text>();
		player = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Flashlight>();

		end.enabled = false;

		defeat = new Color(178/255f, 1/255f, 1/255f, 100/255f);
		victory = new Color(60/255f, 1f, 60/255f, 100/255f);
	}

	// Update is called once per frame
	void Update () {
	}

	public void displayStats(float time, int endPower, int endBatteries, bool alive){
		endT = time;
		endP = endPower;
		endB = endBatteries;

		end.enabled = true;
		msg.text = "YOU ESCAPED!";

		int hiscoreP = PlayerPrefs.GetInt ("hiscoreP", 0);
		int hiscoreB = PlayerPrefs.GetInt ("hiscoreB", 0);
		float hiscoreT = PlayerPrefs.GetFloat ("hiscoreT", 0f);

		//strings to be printed in UI
		string bestP = hiscoreP.ToString ();
		string bestB = hiscoreB.ToString ();

		string currP = endPower.ToString ();
		string currB = endBatteries.ToString ();

		//convert float to minutes and seconds
		int[] bestTint = minuteSeconds (hiscoreT);
		int[] currTint = minuteSeconds (time);

		string bestT = bestTint[0].ToString()+"m"+bestTint[1].ToString()+"s";

		string currT = currTint[0].ToString()+"m"+currTint[1].ToString()+"s";

		if (alive) {
			GetComponent<Image> ().color = victory;

			if (endPower > hiscoreP) { 
				PlayerPrefs.SetInt ("hiscoreP", endPower);
				bestP = currP + " (New Record!)";
			}
			if (endBatteries > hiscoreB) {
				PlayerPrefs.SetInt ("hiscoreB", endBatteries);
				bestB = currB + " (New Record!)";
			}
			if (time < hiscoreT || hiscoreT <=0) {
				PlayerPrefs.SetFloat ("hiscoreT", time);
				bestT = currT + " (New Record!)";
			}

		} else {
			GetComponent<Image> ().color = defeat;

			msg.text = "YOU DIED";

		}

		currStats.text = "\n" + currT + "\n" + currP + "\n" + currB;

		bestStats.text = "Personal Bests\n" + bestT + "\n" + bestP + "\n" + bestB;
	}

	int[] minuteSeconds(float time){
		int minutes = Mathf.FloorToInt (time / 60);
		int seconds = Mathf.RoundToInt (time % 60);

		return new int[] {minutes, seconds};
	}

	public void resetStats(){
		Debug.Log ("reset pushed");
		PlayerPrefs.DeleteKey ("hiscoreP");
		PlayerPrefs.DeleteKey ("hiscoreB");
		PlayerPrefs.DeleteKey ("hiscoreT");

		//update stats with current run stats
		if (player.wonGame) {
			PlayerPrefs.SetInt ("hiscoreP", endP);
			PlayerPrefs.SetInt ("hiscoreB", endB);
			PlayerPrefs.SetFloat ("hiscoreT", endT);
		}

		int hiscoreP = PlayerPrefs.GetInt ("hiscoreP", 0);
		int hiscoreB = PlayerPrefs.GetInt ("hiscoreB", 0);
		float hiscoreT = PlayerPrefs.GetFloat ("hiscoreT", 0f);

		int[] bestTint = minuteSeconds (hiscoreT);
		string bestT = bestTint[0].ToString()+"m"+bestTint[1].ToString()+"s";

		bestStats.text = "Personal Bests\n" + bestT.ToString() + "\n" + hiscoreP.ToString() + "\n" + hiscoreB.ToString();
	}
}
