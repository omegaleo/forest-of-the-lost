using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleScreenButton : MonoBehaviour
{
    [SerializeField] private GameObject selectionArrow;

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            selectionArrow.SetActive(true);
        }
        else
        {
            selectionArrow.SetActive(false);
        }
    }
}
