using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityLibrary;

public class Controller : MonoBehaviour
{
    [SerializeField] Button speechButton;
    [SerializeField] Button[] voiceBtn;
    [SerializeField] string[] voiceID;

    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI volumeText;
    [SerializeField] Slider pitchSlider;
    [SerializeField] TextMeshProUGUI pitchText;

    [SerializeField] TMP_InputField textField;
    [SerializeField] AudioSource source;
    //Singleton
    public static Controller Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        speechButton.onClick.AddListener(delegate { SpeechRun(); });
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeSliderChanged(); });
        pitchSlider.onValueChanged.AddListener(delegate { OnPitchSliderChanged(); });

        for(int i = 0; i< voiceBtn.Length; i++)
        {
            int j = i;
            voiceBtn[i].onClick.AddListener(delegate { ChangeVoice(j); });
        }

        ChangeVoice(0);

        Clear();
    }

    [SerializeField] GameObject taskCreationPopup;
    [SerializeField] GameObject sliderTaskCreationPopup;
    [SerializeField] GameObject toggleTaskCreationPopup;
    

    public void OnValueChanged()
    {

    }

    public void ChangeVoice(int index)
    {
        for(int i = 0; i<voiceBtn.Length; i++)
        {
            var color = voiceBtn[i].colors;
            color.normalColor = new Color(1, 1, 1, 1);
            color.selectedColor = new Color(1, 1, 1, 1);
            voiceBtn[i].colors = color;
        }
        var btnColor = voiceBtn[index].colors;
        btnColor.normalColor = new Color(0.08f, 0.896f, 0, 1);
        btnColor.selectedColor = new Color(0.08f, 0.896f, 0, 1);
        voiceBtn[index].colors = btnColor;

        Speech.instance.voiceID = voiceID[index];
        Speech.instance.SetVoice();
        source.Stop();
    }

    public void OnVolumeSliderChanged()
    {
        source.volume = volumeSlider.value;
        volumeText.text = volumeSlider.value.ToString("0.00") + "%";
    }

    public void OnPitchSliderChanged()
    {
        source.pitch = volumeSlider.value;
        pitchText.text = pitchSlider.value.ToString("0.00");
    }

    public void SpeechRun()
    {
        Speech.instance.Say(textField.text, TTSCallback);
    }

    void TTSCallback(string message, AudioClip audio)
    {
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.volume = volumeSlider.value;
        source.pitch = pitchSlider.value;
        source.clip = audio;
        source.Play();
    }


    public void Clear()
    {
        volumeSlider.value = 0.8f;
        volumeSlider.maxValue = 1;
        volumeSlider.minValue = 0;
        
        pitchSlider.value = 0.85f;
        pitchSlider.maxValue = 3;
        pitchSlider.minValue = -3;
    }



    public void Quit()
    {
        Clear();
        Application.Quit();
    }
}
