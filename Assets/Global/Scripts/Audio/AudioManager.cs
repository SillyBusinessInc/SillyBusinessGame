using UnityEngine;

public class AudioManager : Reference
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip bradleySweepRVoice;

    public AudioClip bradleySweepLVoice;
    
    public AudioClip bradleyPoundVoice;
    
    public AudioClip crumbPickup;
    
    public AudioClip hitEnemy;
    
    public AudioClip poundAttackSFX;
    
    public AudioClip hitCollision;
    
    public AudioClip bradleyGetsHurt;
    
    public AudioClip powerUpPickUp;
    
    public AudioClip healItemPickup;

    public AudioClip dashSfx;

    public AudioClip deathSfx;

    public AudioClip gameOverScreenSFX;
    public AudioClip calorieSFX;

    public AudioClip enemyThankYousfx;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}