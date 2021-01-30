using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField] private AudioClip clickSFX;

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);

        if(clickSFX != null)
        {
            AudioController.instance.PlayClip(AudioController.Source.SFX, clickSFX);
        }
    }

    public void ExitGame()
    {
        Application.Quit();

        if (clickSFX != null)
        {
            AudioController.instance.PlayClip(AudioController.Source.SFX, clickSFX);
        }
    }
}
