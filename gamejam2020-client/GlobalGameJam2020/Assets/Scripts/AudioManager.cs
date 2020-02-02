using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum SoundEffectType
{
    Deposit,
    Explosion,
    PlayerSpawn,
    PickupSound
}

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] audioLevels;
    
    public GameManager gameManager;

    public AudioClip explosionSound;
    public AudioClip depositSound;
    public AudioClip playerSpawnSound;
    public AudioClip pickupSound;

    private int lastDifficulty = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.Log("Audio Source not found!");
        }

        gameManager = GameObject.FindObjectOfType<GameManager>();
        
        if (gameManager == null)
        {
            Debug.Log("GameManager not found!");
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            var volume = PlayerPrefs.GetFloat("volume");

            audioSource.volume = volume;
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    
    void Update()
    {
        if (audioSource == null || audioLevels.Length == 0)
        {
            return;
        }
        
        if (gameManager == null)
        {
            if (audioLevels.Length > 0 && lastDifficulty != 0) 
            {
                audioSource.clip = audioLevels[0];
                lastDifficulty = 0;
            }

            return;
        } 
        
        var difficulty = gameManager.GetDifficultyLevel();

        if (difficulty == 0)
        {
            difficulty = 1;
        }

        if (lastDifficulty != difficulty)
        {
            if (difficulty < 1 || difficulty > audioLevels.Length)
            {
                difficulty = 0;
                Debug.Log("Invalid difficulty index");
            }
            
            audioSource.clip = audioLevels[difficulty-1];

            Debug.Log("Audio change - Playing " + audioLevels[difficulty -1].name);
            lastDifficulty = difficulty;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySoundEffect(SoundEffectType type)
    {
        switch (type)
        {
            case SoundEffectType.Deposit:
                if (depositSound != null)
                {
                    audioSource.PlayOneShot(depositSound);
                }

                break;
            case SoundEffectType.Explosion:
                if (explosionSound != null)
                {
                    audioSource.PlayOneShot(explosionSound);
                }

                break;
            case SoundEffectType.PlayerSpawn:
                if (playerSpawnSound != null)
                {
                    audioSource.PlayOneShot(playerSpawnSound);
                }

                break;
            case SoundEffectType.PickupSound:
                if (pickupSound != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }

                break;
            default:
                break;
        }
        
    }

    public void PlayTrack(int trackNumber)
    {
        if (trackNumber < 0 || trackNumber > audioLevels.Length - 1)
        {
            Debug.Log("Invalid track number");
            return;
        }
        
        if (PlayerPrefs.HasKey("volume"))
        {
            var volume = PlayerPrefs.GetFloat("volume");

            audioSource.volume = volume;
        }

        audioSource.clip = audioLevels[trackNumber];
        audioSource.loop = true;
        audioSource.Play();
    }
    
}
