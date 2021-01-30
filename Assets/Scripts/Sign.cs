using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueManager;

public class Sign : MonoBehaviour
{
    bool hasCollided = false;
    [SerializeField] private List<Dialogue> messages = new List<Dialogue>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            DialogueManager.instance.sentences.AddRange(messages);
            hasCollided = true;
        }
    }

    private void Start()
    {
        messages.Add(new Dialogue()
        {
            charName = "",
            message = "To jump press " + InputHandler.instance.GetKey("JUMP")
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && DialogueManager.instance.sentences.Count == 0)
        {
            //Destroy(this.gameObject);
            StartCoroutine(ResetInteract());
        }
    }

    IEnumerator ResetInteract()
    {
        yield return new WaitForSeconds(2.5f);
        hasCollided = false;
    }
}
