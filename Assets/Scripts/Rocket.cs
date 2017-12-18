
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSourceThrust;

   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float rocketThrust = 100f;

	// Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSourceThrust = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Rotate();
        Thrust();
       
	}

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            default:
                print("DEAD");
                break;
                
        }
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
