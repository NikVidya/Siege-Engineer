using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicManager : Singleton<CinematicManager> {

    Queue<string> cinematicQueue = new Queue<string> ();

    string activeCinematicSceneName = null;

    void AdvanceCinematicQueue () {
        AdvanceCinematicQueue(true);
        // Only advance if there is no active cinematic
        // if (string.IsNullOrEmpty (activeCinematicSceneName)) {
        //     LoadCinematic (cinematicQueue.Dequeue ());
        // }
    }
    void AdvanceCinematicQueue (bool additive) {
        // Only advance if there is no active cinematic
        if (string.IsNullOrEmpty (activeCinematicSceneName)) {
            LoadCinematic (cinematicQueue.Dequeue (), additive);
        }
    }

    void LoadCinematic (string sceneName) {
        LoadCinematic(sceneName, true);
        // SceneManager.LoadScene (sceneName, LoadSceneMode.Additive); // Add the cinematic onto the scene
        // activeCinematicSceneName = sceneName;
    }
    void LoadCinematic (string sceneName, bool additive) {
        if (additive) {
            SceneManager.LoadScene (sceneName, LoadSceneMode.Additive); // Add the cinematic onto the scene
            activeCinematicSceneName = sceneName;
        } else {
            Debug.Log("ohh we got here at least");
            SceneManager.LoadScene (sceneName, LoadSceneMode.Single); // Add the cinematic onto the scene
            activeCinematicSceneName = sceneName;
        }
    }

    public void EnqueueCinematic (string narrativeSceneName) {
        EnqueueCinematic(narrativeSceneName, true);
        // cinematicQueue.Enqueue (narrativeSceneName);
        // AdvanceCinematicQueue ();
    }
    public void EnqueueCinematic (string narrativeSceneName, bool additive) {
        cinematicQueue.Enqueue (narrativeSceneName);
        AdvanceCinematicQueue (additive);
    }

    public void OnCinematicFinished () {
        // Unload cinematic
        OnCinematicFinished(true);
        // if (!string.IsNullOrEmpty (activeCinematicSceneName)) {
        //     SceneManager.UnloadSceneAsync (activeCinematicSceneName);
        // } else {
        //     Debug.LogError ("Finished a cinematic while there was no active scene!");
        // }
        // activeCinematicSceneName = null;
        // AdvanceCinematicQueue ();
    }
    public void OnCinematicFinished(bool additive) {
        if (!string.IsNullOrEmpty (activeCinematicSceneName)) {
            SceneManager.UnloadSceneAsync (activeCinematicSceneName);
        } else {
            Debug.LogError ("Finished a cinematic while there was no active scene!");
        }
        activeCinematicSceneName = null;
        AdvanceCinematicQueue (additive);
    }
}