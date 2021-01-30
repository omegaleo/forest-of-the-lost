using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[ExecuteAlways]
public class ThemeManager : MonoBehaviour
{
    [SerializeField] private Theme theme;

    /*private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }*/

    private void Update()
    {
        foreach (string tag in theme.tagsToCheck)
        {
            List<GameObject> objects = GameObject.FindGameObjectsWithTag(tag).ToList();
            foreach (GameObject element in objects)
            {
                if (element.HasComponent<Text>())
                {
                    Text text = element.GetComponent<Text>();
                    text.color = theme.textColorHex.ColorFromHexString();
                    text.font = theme.font != null ? theme.font : text.font;

                    if (theme.hasShadow)
                    {
                        if (!element.HasComponent<Shadow>()) element.AddComponent<Shadow>();

                        Shadow shadow = element.GetComponent<Shadow>();
                        shadow.effectColor = theme.shadowColorHex.ColorFromHexString();
                        shadow.effectDistance = theme.shadowDistance;
                    }
                }

                if (element.HasComponent<Button>())
                {
                    Button btn = element.GetComponent<Button>();
                    btn.colors = theme.GetButtonColors();
                    if (element.HasComponent<Image>())
                    {
                        Image img = element.GetComponent<Image>();
                        img.color = btn.colors.normalColor;
                        img.sprite = theme.btnBackground != null ? theme.btnBackground : img.sprite;
                    }
                }
            }
        }

    }
}