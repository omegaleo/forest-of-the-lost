using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite lanternSprite;
    public bool isLanternEnabled;
    public bool isLanternOn;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Light light;
    [SerializeField] private PlayerMovement movementController;

    [Header("Stats")]
    [SerializeField] private float lives = 3; //each hit = 0.5f or 0.25f damage
    [SerializeField] private float maxLives = 3;
    [SerializeField] private Vector3 checkpoint;
    [SerializeField] private float energy = 1.0f;
    [SerializeField] private float energySpend = 0.1f;
    [SerializeField] private float energyGain = 0.0025f;

    public static Player instance;
    public bool isDead = false;

    [Header("Sounds")]
    [SerializeField] private AudioClip walkOnGrass;

    

    public float GetLivesCount()
    {
        return lives;
    }

    public float GetMaxLivesCount()
    {
        return maxLives;
    }

    public float GetEnergy()
    {
        return energy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Damage(0.25f);
            movementController.JumpBack();
            StartCoroutine(FlickerSprite());
        }

        if(collision.gameObject.CompareTag("Death"))
        {
            lives -= 1.0f;
            if(lives > 0.0f)
            {
                transform.localPosition = checkpoint;
                StartCoroutine(FlickerSprite());
            }
            else
            {
                //Player is dead
                Dead();
            }
        }
    }

    bool isClipPlaying = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grass"))
        {
            if (movementController.isMoving && !isClipPlaying)
            {
                AudioController.instance.PlayClip(AudioController.Source.SFX, walkOnGrass);
                isClipPlaying = true;
            }
            else if(!movementController.isMoving)
            {
                AudioController.instance.Stop(AudioController.Source.SFX);
                isClipPlaying = false;
            }
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.collider.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }

    IEnumerator FlickerSprite()
    {
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
    }


    public void Damage(float damage)
    {
        lives -= damage;

        if(lives <= 0)
        {
            //Player is dead
            Dead();
        }
    }

    public void Dead()
    {
        isDead = true;
        GameOver.instance.Show();
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        checkpoint = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Action") && DialogueManager.instance.sentences.Count == 0 && !isDead)
        {
            if(energy > 0.13f) //because of display
                ToggleLantern();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if(isLanternOn)
        {
            if(energy <= 0.13f)
            {
                ToggleLantern();
            }
            else
            {
                energy -= (energySpend) * Time.deltaTime;
            }
        }
        else
        {
            float increase = (energyGain) * Time.deltaTime;
            if(energy + increase <= 1.0f)
            {
                energy += increase;
            }
            else
            {
                energy = 1.0f;
            }
        }
    }

    public void ToggleLantern()
    {
        if(isLanternEnabled)
        {
            if(isLanternOn)
            {
                renderer.sprite = normalSprite;
                light.enabled = false;
                isLanternOn = false;
            }
            else
            {
                renderer.sprite = lanternSprite;
                light.enabled = true;
                isLanternOn = true;
            }
            AudioController.instance.PlaySFX(0);
        }
    }

    public void SetCheckpoint()
    {
        checkpoint = this.transform.localPosition;
    }
}
