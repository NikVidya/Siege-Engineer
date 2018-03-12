using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicManager : Singleton<CinematicManager> {

    Queue<string> cinematicQueue = new Queue<string>();

    Scene? activeCinematic = null;

    void AdvanceCinematicQueue()
    {
        // Only advance if there is no active cinematic
        if (activeCinematic == null)
        {
            LoadCinematic(cinematicQueue.Dequeue());
        }
    }

    void LoadCinematic(string sceneName)
    {
        Scene cinematic = SceneManager.GetSceneByName(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive); // Add the cinematic onto the scene
        activeCinematic = cinematic;
    }

    public void EnqueueCinematic(string narrativeSceneName)
    {
        cinematicQueue.Enqueue(narrativeSceneName);
        AdvanceCinematicQueue();
    }

    public void OnCinematicFinished()
    {
        // Unload cinematic
        if(activeCinematic != null)
        {
            SceneManager.UnloadSceneAsync(activeCinematic.GetValueOrDefault());
        }
        else
        {
            Debug.LogError("Finished a cinematic while there was no active scene!");
        }
        activeCinematic = null;
        AdvanceCinematicQueue();
    }
}
