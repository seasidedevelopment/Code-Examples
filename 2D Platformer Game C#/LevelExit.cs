using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    float _levelLoadDelay = 1f;

   void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }

       
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(_levelLoadDelay);
        
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int _nextSceneIndex = _currentSceneIndex + 1;

        if(_nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            _nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(_nextSceneIndex);
    }
}
