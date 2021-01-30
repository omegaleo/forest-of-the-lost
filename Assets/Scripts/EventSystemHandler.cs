using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemHandler : MonoBehaviour
{
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            if(EventSystem.current.firstSelectedGameObject)
            {
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            }
        }
    }
}
