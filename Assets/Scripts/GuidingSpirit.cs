using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueManager;

public class GuidingSpirit : MonoBehaviour
{

    bool hasCollided = false;

    [SerializeField] private List<Dialogue> messages = new List<Dialogue>();
    [SerializeField] private SpriteRenderer img;
    [SerializeField] private Light light;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private float colorSpeed = 0.05f;
    bool color1On = false;
    bool color2On = true;


    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Vector3 goingTo;
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private float speed;
    [SerializeField] private float margin = 0.1f;
    [SerializeField] bool movingToStart = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            DialogueManager.instance.sentences.AddRange(messages);
            hasCollided = true;
        }
    }

    private void Awake()
    {
        startingPosition = this.transform.localPosition;
        goingTo = new Vector3(startingPosition.x, startingPosition.y + distance, startingPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasCollided && DialogueManager.instance.sentences.Count == 0)
        {
            string keyToPress = InputHandler.instance.GetKey("ACTION");

            Player.instance.isLanternEnabled = true;
            DialogueManager.instance.sentences.Add(new Dialogue()
            {
                charName = "Spirit",
                message = "To use the lantern just press " + keyToPress + "."
            });
            Destroy(this.gameObject);
        }

        if(img.color == color1)
        {
            color1On = false;
            color2On = true;
        }
        else if(img.color == color2)
        {
            color1On = true;
            color2On = false;
        }

        if(color1On)
        {
            img.color = Color.Lerp(img.color, color1, colorSpeed);
            light.color = Color.Lerp(img.color, color1, colorSpeed);
        }
        if(color2On)
        {
            img.color = Color.Lerp(img.color, color2, colorSpeed);
            light.color = Color.Lerp(img.color, color2, colorSpeed);
        }


        if (this.transform.localPosition.y <= startingPosition.y + margin)
        {
            movingToStart = false;
        }

        if (this.transform.localPosition.y >= goingTo.y - margin)
        {
            movingToStart = true;
        }

        if (!movingToStart)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, goingTo, speed);
        }
        else
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, startingPosition, speed);
        }
    }
}
