
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSourceThrust;

	// Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSourceThrust = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);

            if (!audioSourceThrust.isPlaying)
            {
                audioSourceThrust.Play();
            }
        }

        else
        {
            audioSourceThrust.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
           transform.Rotate(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.right);
        }
    }
}
