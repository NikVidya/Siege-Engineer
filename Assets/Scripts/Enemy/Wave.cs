using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
Gameplay occurs in "waves". A "wave" will cause damage to structures and different events to occur at intervals.
Waves will handle timers
 */
public class Wave : MonoBehaviour {
    //public
    [Tooltip ("Wave duration in seconds")]
    public float waveDuration = 120;
    [Tooltip ("REST waves will ignore grace period")]
    public float gracePeriod = 5;
    [Tooltip ("The display for the duration of the wave. Can be shared by other waves")]
    public Text waveDurationUI;
    // Wave type determines behaviouor. Barrage waves, for example, simply hit the player with loads of bombardments. Regular waves damage structures.
    public enum WaveType {
        REGULAR,
        BARRAGE,
        REST,
        HOLDOUT
    }
    public WaveType Type {
        get {
            return typeOfWave;
        }
        set { }
    }

    [Tooltip ("REGULAR: Periodic damage to structures \nBARRAGE: No damage to structures, player must survive a barrage\nREST: Nothing happens, player can chill out\nHOLDOUT: Timer increases indefinitely, player must hold out for as long as possible")]
    public WaveType typeOfWave;
    [Header ("Damageable objects")]
    [Tooltip ("The gate game object")]
    public Damageable gate;
    [Tooltip ("The amount of damage the gate takes per second")]
    public int gateDamage = 3;
    [Tooltip ("The trebuchet game objects")]
    public Damageable[] trebuchets;
    [Tooltip ("The amount of damage the trebuchets take per second")]
    public int trebuchetDamage = 3;
    [Tooltip ("If this is a BARRAGE wave, this determines time between each bombardment")]
    public float timeBetweenBombardments = 0.7f;
    public UnityEvent finish;
    public enum WaveState {
        WAITING,
        BEGUN,
        FINISHED
    }
    public WaveState State {
        get {
            return waveState;
        }
        set { }
    }
    private WaveState waveState = WaveState.WAITING;
    //private
    private string timerText;
    private int timeMinutes;
    private float timeSeconds;
    private bool grace = true;
    private float graceTime = 0;

    void Start () {
        graceTime = gracePeriod;
    }
    public void BeginWave () {
        waveState = WaveState.BEGUN;
        Debug.Log (name + " Wave Begun!");
        if (typeOfWave != WaveType.HOLDOUT) {
            timeMinutes = (int) Mathf.Floor (waveDuration / 60);
            timeSeconds = waveDuration - (timeMinutes * 60);
            waveDurationUI.text = timeMinutes + ":" + timeSeconds;
        }
        switch (typeOfWave) {
            case WaveType.REGULAR:
                InvokeRepeating ("StructureDamage", gracePeriod, 1);
                break;
            case WaveType.BARRAGE:
                InvokeRepeating ("Barrage", gracePeriod, timeBetweenBombardments);
                break;
        }
    }
    void Update () {
        if (waveState == WaveState.BEGUN) {
            switch (typeOfWave) {
                case WaveType.REGULAR:
                    TimerLogicRegular ();
                    break;
                case WaveType.BARRAGE:
                    TimerLogicRegular ();
                    break;
                case WaveType.REST:
                    TimerLogicRest ();
                    break;
                case WaveType.HOLDOUT:
                    TimerLogicHoldout ();
                    break;

            }
        }
    }

    void TimerLogicRegular () {
        if (grace) {
            graceTime -= Time.deltaTime;
            waveDurationUI.text = "WAVE BEGINS IN: " + Mathf.Floor (graceTime);
            if (graceTime <= 0) {
                grace = false;
            }
        } else {
            waveDuration -= Time.deltaTime;

            // timer ui code
            timeMinutes = (int) Mathf.Floor (waveDuration / 60);
            timeSeconds = waveDuration - (timeMinutes * 60);
            string zero = "";
            if (timeSeconds < 10) { zero = "0"; } else { zero = ""; }

            waveDurationUI.text = timeMinutes + ":" + zero + Mathf.Floor (timeSeconds);

            if (waveDuration <= 0) {
                Finish ();
            }
        }
    }
    void TimerLogicRest () {
        waveDuration -= Time.deltaTime;

        // timer ui code
        timeMinutes = (int) Mathf.Floor (waveDuration / 60);
        timeSeconds = waveDuration - (timeMinutes * 60);
        string zero = "";
        if (timeSeconds < 10) { zero = "0"; } else { zero = ""; }
        waveDurationUI.text = "Rest Time: " + timeMinutes + ":" + zero + Mathf.Floor (timeSeconds);
        if (waveDuration <= 0) {
            Finish ();
        }
    }
    void TimerLogicHoldout () {
        waveDuration += Time.deltaTime;
        // timer ui code
        timeMinutes = (int) Mathf.Floor (waveDuration / 60);
        timeSeconds = waveDuration - (timeMinutes * 60);
        string zero = "";
        if (timeSeconds < 10) { zero = "0"; } else { zero = ""; }
        waveDurationUI.text = timeMinutes + ":" + zero + Mathf.Floor (timeSeconds);
    }

    // Damage occurs per second
    void StructureDamage () {
        gate.ChangeHealth (-gateDamage);
        for (int i = 0; i < trebuchets.Length; i++) {
            trebuchets[i].ChangeHealth (-trebuchetDamage);
        }
    }

    // Barrages the player
    void Barrage () {
        BattleManager.Instance.SpawnBombardment ();
    }

    void Finish () {
        CancelInvoke ("StructureDamage");
        waveState = WaveState.FINISHED;
        if (finish == null) {
            finish = new UnityEvent ();
        }
        finish.Invoke ();
    }
}