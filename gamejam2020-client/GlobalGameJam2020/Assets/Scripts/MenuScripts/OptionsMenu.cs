using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts
{
    public class OptionsMenu : MonoBehaviour
    {
        private AudioManager audioManager;
        public Slider slider;
        public InputField inputText;
        public TMP_Text percentBox;

        public void Start()
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
            
            if (inputText != null)
            {
                inputText.text = PlayerPrefs.GetString("serverUrl");
            }

            if (!PlayerPrefs.HasKey("volume"))
            {
                PlayerPrefs.SetFloat("volume", 100f);
                PlayerPrefs.Save();
            }
            
            if (slider != null)
            {
                slider.value = PlayerPrefs.GetFloat("volume");
            }
        }

        public void UrlSubmitter()
        {
            PlayerPrefs.SetFloat("volume", slider.value);
            PlayerPrefs.SetString("serverUrl", inputText.text);
            PlayerPrefs.Save();
            
        }

        public void VolumeControl()
        {
            percentBox.text = (slider.value * 100).ToString("0");
            
            audioManager.SetVolume(slider.value * 100);
        }
    }
}
