using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text messageText;
    public Text buttonText;
    
    [System.Serializable]
    public class Dialogue
    {
        public string charName;
        public string message;
    }

    public List<Dialogue> sentences;
    public int index;
    public float typingSpeed;

    public static DialogueManager instance;

    public Vector3 hiddenPosition;
    public Vector3 shownPosition;

    bool typing = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        nameText.text = "";
        messageText.text = "";
        #if !UNITY_WEBGL
        shownPosition = transform.localPosition;
        hiddenPosition = new Vector3(shownPosition.x, shownPosition.y + 300, shownPosition.z);
        transform.localPosition = hiddenPosition;
        #else //In WEBGL the scrolling effect doesn't work properly
        nameText.enabled = false;
        messageText.enabled = false;
        buttonText.enabled = false;
        this.GetComponent<Image>().enabled = false;
        #endif
    }

    private void Update()
    {
        if(sentences.Count > 0)
        {
#if !UNITY_WEBGL
            transform.localPosition = Vector3.Lerp(transform.localPosition, shownPosition, 0.125f);
#else
            nameText.enabled = true;
            messageText.enabled = true;
            buttonText.enabled = true;
            this.GetComponent<Image>().enabled = true;
#endif
            if (!typing)
            {
                StartCoroutine(Type());
                typing = true;
            }
        }
        else
        {
#if !UNITY_WEBGL
            transform.localPosition = Vector3.Lerp(transform.localPosition, hiddenPosition, 0.125f);
#else
            nameText.enabled = false;
            messageText.enabled = false;
            buttonText.enabled = false;
            this.GetComponent<Image>().enabled = false;
#endif
        }

        if (Input.GetButtonDown("Action"))
        {
            if(sentences.Count > 0)
            {
                if (messageText.text == sentences[index].message)
                {
                    if (index + 1 < sentences.Count)
                    {
                        if (Input.GetButtonDown("Action"))
                        {
                            messageText.text = "";
                            index++;
                            StartCoroutine(Type());
                        }
                    }
                    else
                    {
                        sentences.Clear();
                        typing = false;
                        nameText.text = "";
                        messageText.text = "";
                        index = 0;
                    }
                }
            }
        }
    }


    IEnumerator Type()
    {
        nameText.text = sentences[index].charName;
        buttonText.text = "";
        foreach(char letter in sentences[index].message.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        buttonText.text = InputHandler.instance.GetKey("Action");
    }

    public void AddSentence(Dialogue dialogue)
    {
        sentences.Add(dialogue);
    }
}
