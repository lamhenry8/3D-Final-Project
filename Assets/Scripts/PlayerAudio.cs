using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private float footstepVolume = 0.5f;
    [SerializeField] private float volumeRandomness = 0.1f;
    [SerializeField] private float walkInterval = 0.5f;
    [SerializeField] private float runInterval = 0.3f;

    private AudioSource audioSource;
    private Animator animator;
    private float timeSinceLastFootstep = 0f;
    private readonly int speedHash = Animator.StringToHash("Speed");

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator == null)
        {
            return;
        }

        float speed = animator.GetFloat(speedHash);

        if (speed > 0.1f)
        {
            float currentInterval = speed > 4f ? runInterval : walkInterval;
            timeSinceLastFootstep += Time.deltaTime;

            if (timeSinceLastFootstep >= currentInterval)
            {
                PlayFootstep();
                timeSinceLastFootstep = 0f;
            }
        }
        else
        {
            timeSinceLastFootstep = 0f;
        }
    }

    public void PlayFootstep()
    {
        if (footstepClips.Length == 0 || audioSource == null)
        {
            return;
        }

        AudioClip randomClip = footstepClips[Random.Range(0, footstepClips.Length)];
        float randomVolume = footstepVolume + Random.Range(-volumeRandomness, volumeRandomness);
        randomVolume = Mathf.Clamp01(randomVolume);

        audioSource.PlayOneShot(randomClip, randomVolume);
    }
}
