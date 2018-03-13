using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicManager : Singleton<CinematicManager> {

    Queue<string> cinematicQueue = new Queue<string>();

    string activeCinematicSceneName = null;

    void AdvanceCinematicQueue()
    {
        // Only advance if there is no active cinematic
        if (string.IsNullOrEmpty(activeCinematicSceneName))
        {
            LoadCinematic(cinematicQueue.Dequeue());
        }
    }

    void LoadCinematic(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive); // Add the cinematic onto the scene
        activeCinematicSceneName = sceneName;
    }

    public void EnqueueCinematic(string narrativeSceneName)
    {
        cinematicQueue.Enqueue(narrativeSceneName);
        AdvanceCinematicQueue();
    }

    public void OnCinematicFinished()
    {
        // Unload cinematic
        if(!string.IsNullOrEmpty(activeCinematicSceneName))
        {
            SceneManager.UnloadSceneAsync(activeCinematicSceneName);
        }
        else
        {
            Debug.LogError("Finished a cinematic while there was no active scene!");
        }
        activeCinematicSceneName = null;
        AdvanceCinematicQueue();
    }
}
