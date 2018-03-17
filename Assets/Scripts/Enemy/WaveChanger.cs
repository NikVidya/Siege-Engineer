using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChanger : MonoBehaviour {

    [Tooltip ("List of waves to iterate through. Once the last wave is over, the level finishes")]
    public Wave[] waves;
    private int currentWave = 0;
    void Start () {
        waves[currentWave].BeginWave ();
    }

    void Update () {
        if (waves[currentWave].State == Wave.WaveState.FINISHED) {
            NextWave ();
        }
    }
    public void NextWave () {
        if (currentWave != waves.Length - 1) {
            currentWave++;
            waves[currentWave].BeginWave ();
        } else {
            GameStateSwitcher.Instance.Victory ();
        }
    }

}