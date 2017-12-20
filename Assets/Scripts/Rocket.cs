
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSourceThrust;

   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float rocketThrust = 100f;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

	// Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSourceThrust = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( state == State.Alive )
        {
            Rotate();
            Thrust();
        }
       
       
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
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadStart", 1f);
                break;

        }
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadStart()
    {
        SceneManager.LoadScene(0);
    }


    private void Rotate()
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


    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame = rocketThrust * Time.deltaTime;

            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);

            if (!audioSourceThrust.isPlaying)
            {
                audioSourceThrust.Play();
            }
        }

        else
        {
            audioSourceThrust.Stop();
        }
    }
}
