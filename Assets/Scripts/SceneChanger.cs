using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] float timer = 1f;

    public void StartLoadSequence()
    {
        StartCoroutine(LoadSequence());
    }

    public void StartEndSequence()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator LoadSequence()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(timer);
        Application.Quit();
    }
}
