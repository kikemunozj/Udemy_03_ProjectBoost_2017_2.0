
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float rocketThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip levelLoad;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem levelLoadParticles;



    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

	// Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( state == State.Alive )
        {
            RespondToRotateInput();
            RespondToThrustInput();
        }
        else
            rigidBody.freezeRotation = false;
       
	}

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
               
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartDeathSequence();
                break;

        }
    }
    private void StartNextLevelSequence()
    {
        levelLoadParticles.Play();
        state = State.Transcending;
        Invoke("LoadNextScene", levelLoadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(levelLoad);
    }

    private void StartDeathSequence()
    {
        deathParticles.Play();
        state = State.Dying;
        Invoke("LoadStart", levelLoadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(death);
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadStart()
    {
        SceneManager.LoadScene(0);
    }


    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = false;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.left * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.right * rotationThisFrame);
        }

        rigidBody.freezeRotation = true;
    }


    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }

        else
        {
            mainEngineParticles.Stop();
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = rocketThrust * Time.deltaTime;

        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
}
