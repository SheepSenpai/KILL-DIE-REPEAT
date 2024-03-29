using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMainMenu : MonoBehaviour
{
    public Animator transition;

    public void PlayGame()
    {
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex - 1));

        if(PauseMenu.GamePaused == true)
        {
            Time.timeScale = 1f;
        }
    }

    IEnumerator LoadNextLevel(int levelIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
}
