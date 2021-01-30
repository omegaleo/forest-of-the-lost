using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AudioController;

public class OptionsBar : MonoBehaviour
{
    public enum OptionsBarType { MUSIC, SFX, QUALITY, WINDOW_RES }

    public GameObject decreaseButton;
    public GameObject increaseButton;
    public Image barFill;
    public Image barOverlay;
    public Text value;
    public OptionsBarType barType;
    public List<string> values;
    public Toggle fullScreen;

    private void Update()
    {
        SetBarFills();
    }

    void SetBarFills()
    {
        switch (barType)
        {
            case OptionsBarType.MUSIC:
                if (AudioController.instance != null)
                {
                    if (barFill != null)
                        barFill.fillAmount = AudioController.instance.GetVolume(Source.MUSIC);
                }
                break;
            case OptionsBarType.QUALITY:
                if (value != null)
                {
                    int qualityLevel = QualitySettings.GetQualityLevel();
                    if (values.Count == 0)
                    {
                        values.Add("Fastest");
                        values.Add("Fast");
                        values.Add("Simple");
                        values.Add("Good");
                        values.Add("Beautiful");
                        values.Add("Fantastic");
                    }

                    value.text = values[qualityLevel];
                }
                break;
            case OptionsBarType.SFX:
                if (AudioController.instance != null)
                {
                    if (barFill != null)
                        barFill.fillAmount = AudioController.instance.GetVolume(Source.SFX);
                }
                break;
            case OptionsBarType.WINDOW_RES:
                if (value != null)
                {
                    value.text = Screen.currentResolution.ToString();

                    Resolution[] resolutions = Screen.resolutions;

                    if (values.Count == 0)
                    {
                        values.AddRange(resolutions.Select(x => x.width + " x " + x.height + " @ " + x.refreshRate).ToList());
                        if (fullScreen != null)
                        {
                            fullScreen.isOn = Screen.fullScreen;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public void FullScreenToggle()
    {
        if (fullScreen != null)
        {
            Screen.fullScreen = fullScreen.isOn;
        }
    }

    public void Increase()
    {
        switch (barType)
        {
            case OptionsBarType.MUSIC:
                if (AudioController.instance != null)
                {
                    if (AudioController.instance.GetVolume(Source.MUSIC) + 0.1f <= 1.0f)
                    {
                        AudioController.instance.SetVolume(Source.MUSIC, AudioController.instance.GetVolume(Source.MUSIC) + 0.1f);
                    }
                    else
                    {
                        AudioController.instance.SetVolume(Source.MUSIC, 1.0f);
                    }
                }
                break;
            case OptionsBarType.QUALITY:
                try
                {
                    int currentIndex = QualitySettings.GetQualityLevel();
                    if (currentIndex + 1 < values.Count)
                    {
                        QualitySettings.SetQualityLevel(currentIndex + 1);
                    }
                    else
                    {
                        QualitySettings.SetQualityLevel(0);
                    }
                }
                catch (Exception e)
                {

                }
                break;
            case OptionsBarType.SFX:
                if (AudioController.instance != null)
                {
                    if (AudioController.instance.GetVolume(Source.SFX) + 0.1f <= 1.0f)
                    {
                        AudioController.instance.SetVolume(Source.SFX, AudioController.instance.GetVolume(Source.SFX) + 0.1f);
                    }
                    else
                    {
                        AudioController.instance.SetVolume(Source.SFX, 1.0f);
                    }
                }
                break;
            case OptionsBarType.WINDOW_RES:
                try
                {
                    //1920 x 1080 @ 75Hz
                    string currentRes = Screen.currentResolution.width + " x " + Screen.currentResolution.height + " @ " + Screen.currentResolution.refreshRate;
                    int currentIndex = values.FindIndex(x => x == currentRes);

                    bool isFullScreen = false;

                    if (fullScreen != null)
                    {
                        isFullScreen = fullScreen.isOn;
                    }

                    if (currentIndex + 1 < values.Count)
                    {
                        currentIndex++;
                        string[] split1 = values[currentIndex].Split('x');
                        string[] split2 = split1[1].Split('@');
                        int width = int.Parse(split1[0]);
                        int height = int.Parse(split2[0]);
                        int refreshRate = int.Parse(split2[1]);
                        Screen.SetResolution(width, height, isFullScreen, refreshRate);
                    }
                    else
                    {
                        string[] split1 = values[0].Split('x');
                        string[] split2 = split1[1].Split('@');
                        int width = int.Parse(split1[0]);
                        int height = int.Parse(split2[0]);
                        int refreshRate = int.Parse(split2[1]);
                        Screen.SetResolution(width, height, isFullScreen, refreshRate);
                    }
                }
                catch (Exception e)
                {

                }
                break;
            default:
                break;
        }
    }

    public void Decrease()
    {
        switch (barType)
        {
            case OptionsBarType.MUSIC:
                if (AudioController.instance != null)
                {
                    if (AudioController.instance.GetVolume(Source.MUSIC) - 0.1f >= 0.0f)
                    {
                        AudioController.instance.SetVolume(Source.MUSIC, AudioController.instance.GetVolume(Source.MUSIC) - 0.1f);
                    }
                    else
                    {
                        AudioController.instance.SetVolume(Source.MUSIC, 0.0f);
                    }
                }
                break;
            case OptionsBarType.QUALITY:
                try
                {
                    int currentIndex = QualitySettings.GetQualityLevel();
                    if (currentIndex - 1 >= 0)
                    {
                        QualitySettings.SetQualityLevel(currentIndex - 1);
                    }
                    else
                    {
                        QualitySettings.SetQualityLevel(values.Count - 1);
                    }
                }
                catch (Exception e)
                {

                }
                break;
            case OptionsBarType.SFX:
                if (AudioController.instance != null)
                {
                    if (AudioController.instance.GetVolume(Source.SFX) - 0.1f >= 0.0f)
                    {
                        AudioController.instance.SetVolume(Source.SFX,AudioController.instance.GetVolume(Source.SFX) - 0.1f);
                    }
                    else
                    {
                        AudioController.instance.SetVolume(Source.SFX,0.0f);
                    }
                }
                break;
            case OptionsBarType.WINDOW_RES:
                try
                {
                    //1920 x 1080 @ 75Hz
                    string currentRes = Screen.currentResolution.width + " x " + Screen.currentResolution.height + " @ " + Screen.currentResolution.refreshRate;
                    int currentIndex = values.FindIndex(x => x == currentRes);

                    bool isFullScreen = false;

                    if (fullScreen != null)
                    {
                        isFullScreen = fullScreen.isOn;
                    }

                    if (currentIndex - 1 >= 0)
                    {
                        currentIndex--;
                        string[] split1 = values[currentIndex].Split('x');
                        string[] split2 = split1[1].Split('@');
                        int width = int.Parse(split1[0]);
                        int height = int.Parse(split2[0]);
                        int refreshRate = int.Parse(split2[1]);
                        Screen.SetResolution(width, height, isFullScreen, refreshRate);
                    }
                    else
                    {
                        string[] split1 = values[values.Count - 1].Split('x');
                        string[] split2 = split1[1].Split('@');
                        int width = int.Parse(split1[0]);
                        int height = int.Parse(split2[0]);
                        int refreshRate = int.Parse(split2[1]);
                        Screen.SetResolution(width, height, isFullScreen, refreshRate);
                    }
                }
                catch (Exception e)
                {

                }
                break;
            default:
                break;
        }
    }

}