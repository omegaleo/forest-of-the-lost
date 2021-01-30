using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum DIRECTION { UP, DOWN, LEFT, RIGHT }

    [SerializeField] private DIRECTION direction;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float distance;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float margin = 0.1f;

    [SerializeField] bool movingToStart = false;

    private void Start()
    {
        startingPosition = transform.localPosition;

        switch(direction)
        {
            case DIRECTION.UP:
                endPosition = new Vector3(startingPosition.x, startingPosition.y + distance, startingPosition.z);
                break;
            case DIRECTION.DOWN:
                endPosition = new Vector3(startingPosition.x, startingPosition.y - distance, startingPosition.z);
                break;
            case DIRECTION.LEFT:
                endPosition = new Vector3(startingPosition.x - distance, startingPosition.y, startingPosition.z);
                break;
            case DIRECTION.RIGHT:
                endPosition = new Vector3(startingPosition.x + distance, startingPosition.y, startingPosition.z);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case DIRECTION.UP:
                if (this.transform.localPosition.y <= startingPosition.y + margin)
                {
                    movingToStart = false;
                }

                if (this.transform.localPosition.y >= endPosition.y - margin)
                {
                    movingToStart = true;
                }
                break;
            case DIRECTION.DOWN:
                if (this.transform.localPosition.y >= startingPosition.y + margin)
                {
                    movingToStart = false;
                }

                if (this.transform.localPosition.y <= endPosition.y - margin)
                {
                    movingToStart = true;
                }
                break;
            case DIRECTION.LEFT:
                if (this.transform.localPosition.x >= startingPosition.x + margin)
                {
                    movingToStart = false;
                }

                if (this.transform.localPosition.x <= endPosition.x - margin)
                {
                    movingToStart = true;
                }
                break;
            case DIRECTION.RIGHT:
                if (this.transform.localPosition.x <= startingPosition.x + margin)
                {
                    movingToStart = false;
                }

                if (this.transform.localPosition.x >= endPosition.x - margin)
                {
                    movingToStart = true;
                }
                break;
        }

        if (!movingToStart)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, endPosition, speed);
        }
        else
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, startingPosition, speed);
        }
    }
}
