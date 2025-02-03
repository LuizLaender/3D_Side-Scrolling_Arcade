using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Controls: ")]
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    [Header("Variables: ")]
    [SerializeField] float thrustStrenght;
    [SerializeField] float rotationStrength;

    [Header("SFX: ")]
    [SerializeField] AudioClip thrustSFX;

    [Header("PFX: ")]
    [SerializeField] ParticleSystem MBoosterPFX;
    [SerializeField] ParticleSystem LBoosterPFX;
    [SerializeField] ParticleSystem RBoosterPFX;

    Rigidbody rb;
    [HideInInspector] public AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    public void TryStopBoostersPFX()
    {
        if (RBoosterPFX.isPlaying) RBoosterPFX.Stop();
        if (LBoosterPFX.isPlaying) LBoosterPFX.Stop();
        if (MBoosterPFX.isPlaying) MBoosterPFX.Stop();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else if (!thrust.IsPressed() && audioSource.isPlaying)
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        if (MBoosterPFX.isPlaying) MBoosterPFX.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrenght * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSFX);
        }
        MBoosterPFX.Play();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput > 0)
        {
            TurnLeft();
        }
        else if (rotationInput < 0)
        {
            TurnRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void TurnRight()
    {
        RBoosterPFX.Play();

        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void TurnLeft()
    {
        LBoosterPFX.Play();

        rb.freezeRotation = true;
        transform.Rotate(Vector3.back * rotationStrength * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void StopRotation()
    {
        transform.Rotate(0, 0, 0);
        if (RBoosterPFX.isPlaying) RBoosterPFX.Stop();
        if (LBoosterPFX.isPlaying) LBoosterPFX.Stop();
    }
}
