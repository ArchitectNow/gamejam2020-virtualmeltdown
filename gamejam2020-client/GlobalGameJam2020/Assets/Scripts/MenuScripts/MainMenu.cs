using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class MainMenu : MonoBehaviour
    {

        void Start()
        {
            var audioManager = GameObject.FindObjectOfType<AudioManager>();

            if (audioManager != null)
            {
                audioManager.PlayTrack(1);
            }
        }
        
        public void PlayGame()
        {
            SceneManager.LoadScene("map");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ShowHelp()
        {
            SceneManager.LoadScene("HelpScreen");
        }
        
        public void ShowMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
