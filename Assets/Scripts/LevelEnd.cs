using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LevelLoadSequence());
        }
    }

    IEnumerator LevelLoadSequence()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
