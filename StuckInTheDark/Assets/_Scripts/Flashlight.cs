using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
	public float maxPower = 100;
    public float decayRate;
    public int keysRequired = 3;
    public int keysCollected;
    public bool exitReady;
    public AudioClip batteryPickupFX, keyPickupFX, pulseClip;
    AudioSource aud;
    public bool batteryInRange = false, keyInRange = false;

    public float minEchoTime;
    public float switchTime;
	public bool lightOn = false;
    public ScannerEffectDemo echolocator;
    public Light flash;
    Collider selectedKey, selectedBattery;

	public bool wonGame = false;

	public AudioClip noise1, noise2;

    public float power;
    private float ttl;

    // Use this for initialization
    void Start () {
        aud = GetComponent<AudioSource>();
        echolocator = GetComponent<ScannerEffectDemo>();
        keysCollected = 0;
        exitReady = false;
        power = maxPower;
        ttl = minEchoTime;
        Cursor.visible = false;

		//ambience
		float noise1time = Random.Range (10.0f, 20.0f);
		Invoke ("randomNoise", noise1time);
		Invoke ("randomNoise2", noise1time + Random.Range (20.0f, 30.0f));

    }
	
	// Update is called once per frame
	void Update () {
		//unpaused
		if (Time.timeScale != 0) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (batteryInRange) {
					addBattery ();
					selectedBattery.GetComponent<BatteryBehaviour> ().deleteThis ();
					selectedBattery = null;
					batteryInRange = false;
				}
				if (keyInRange) {
					addKey ();
					selectedKey.GetComponent<KeyBehaviour> ().deleteThis ();
					selectedKey = null;
					keyInRange = false;
				}
			}
			if (Input.GetKeyDown (KeyCode.E) && power > 0) {
				if (lightOn) {
					lightOn = false;
				}
				if (ttl > minEchoTime) {
					echolocator.pulse ();
					power -= 25;
					aud.PlayOneShot (pulseClip);
					ttl = 0;
				}
			}
			if (Input.GetKeyDown (KeyCode.Q) && power > 0) {
				if (ttl > switchTime && !lightOn)
					lightOn = true;
				else
					lightOn = false;
			}
			if (lightOn) {
				flash.enabled = true;
				power -= decayRate * Time.deltaTime;
			} else {
				flash.enabled = false;
			}
			if (power < 0) {
				lightOn = false;
				flash.enabled = false;
			}
		}
        
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale != 0) {
				Time.timeScale = 0;
				Cursor.visible = true;
			} else {
				Time.timeScale = 1;
				Cursor.visible = false;
			}
		}

		ttl += Time.deltaTime;
    }
    public void addKey()
    {
        aud.PlayOneShot(keyPickupFX);
        keysCollected++;
        if (keysCollected >= keysRequired)
            exitReady = true;
    }

    public void addBattery()
    {
        aud.PlayOneShot(batteryPickupFX);
        power += 20;
    }
    void OnTriggerEnter(Collider col)
    {

        switch (col.tag)
        {
        case "Battery":
            batteryInRange = true;
            selectedBattery = col;
            break;
        case "key":
            keyInRange = true;
            selectedKey = col;
            break;
		case "exit":
			if (exitReady)
				win ();
			break;
        }
    }
    void OnTriggerExit(Collider col)
    {
        switch (col.tag)
        {
            case "Battery":
                batteryInRange = false;
                selectedBattery = null;
                break;
            case "key":
                keyInRange = false;
                selectedKey = null;
                break;
        }
    }

	public void win(){
		wonGame = true;
        Application.Quit();
	}

	void randomNoise(){
		aud.PlayOneShot (noise1);
	}

	void randomNoise2(){
		aud.PlayOneShot (noise2);
	}
}
