using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
	private Rigidbody rb;
    public int speed = 10;
	public Text countText;
	private int count  = 0;
	public Text winText;
	public float jump = 10;
	private bool jumpable;

	 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
		jumpable = true;
		Vector3 currentCheckpoint = new Vector3 (5.67f,1.72f,0.93f);



    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


		Vector3 movement = new Vector3(moveHorizontal, 0.0f , moveVertical);

        rb.AddForce(movement*speed);

		if (Input.GetKey ("space") && jumpable == true)
		{
			Vector3 jumping = new Vector3 (0.0f, jump, 0.0f);
			rb.AddForce (jumping, ForceMode.Impulse);
			jumpable = false;
		}



    }

    void OnTriggerEnter(Collider other)
    {
		
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText ();

        }

		 if(other.gameObject.CompareTag("Killzone")) 
			{
			transform.position = currentCheckpoint;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			}
		if (other.gameObject.CompareTag ("Checkpoint"))
		{
			currentCheckpoint = other.transform.position;
		}
			 

			
    }
    
	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 2)
		{
			winText.text = "You Win!";
		}

	}

	void OnCollisionEnter()
	{
		jumpable = true;
	}

}
