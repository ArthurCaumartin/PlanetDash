using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource _fxSource;
    [SerializeField] public AudioClip clipHitMarker;


    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Play(AudioClip clip)
    {
        _fxSource.PlayOneShot(clip);
    }
}