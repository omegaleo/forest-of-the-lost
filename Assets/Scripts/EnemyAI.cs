using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float clipLength;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Vector3 goingTo;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float speed;
    [SerializeField] private float margin = 0.1f;
    

    bool m_FacingRight = false;

    [SerializeField] bool movingToStart = false;

    private void Awake()
    {
        startingPosition = this.transform.localPosition;
        goingTo = new Vector3(startingPosition.x + distance, startingPosition.y, startingPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localPosition.x <= startingPosition.x + margin)
        {
            movingToStart = false;
            m_FacingRight = false;
            Flip();
        }
        
        if (this.transform.localPosition.x >= goingTo.x - margin)
        {
            movingToStart = true;
            m_FacingRight = true;
            Flip();
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

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Dead()
    {
        /*if(audio != null)
        {
            if(clip != null)
            {
                audio.clip = clip;
                audio.Play();
                Destroy(this.gameObject);
            }
        }*/

        Destroy(this.gameObject);
    }
}
