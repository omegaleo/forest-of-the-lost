using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWonDialogue : MonoBehaviour
{
    [SerializeField] private Text text;

    bool isShowing = false;

    public static GameWonDialogue instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            isShowing = false;
        }

        if (isShowing)
        {
            text.enabled = true;
            this.GetComponent<Image>().enabled = true;

            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            text.enabled = false;
            this.GetComponent<Image>().enabled = false;
        }
    }

    public void Show()
    {
        isShowing = true;
    }
}
