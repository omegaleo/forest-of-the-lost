using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image lives;
    [SerializeField] private Image energy;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 2)
        {
            lives.enabled = true;
            energy.enabled = true;
        }
        else
        {
            lives.enabled = false;
            energy.enabled = false;
        }

        if(Player.instance != null)
        {
            lives.fillAmount = CalculateFillFromLives(Player.instance.GetLivesCount(), Player.instance.GetMaxLivesCount());
            energy.fillAmount = Player.instance.GetEnergy();
        }
    }


    float CalculateFillFromLives(float lives, float max)
    {
        float result = ((lives * 100.0f) / max) / 100.0f;
        return result;
    }
}
