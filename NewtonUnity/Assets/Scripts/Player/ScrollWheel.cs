
//using UnityEngine;

//public class ScrollWheel : MonoBehaviour
//{
//    public float gravityAmount = 9.81f; 
//    public float gravityMultiplier;
//    private float gravityIncrement = 0.1f;
//    Rigidbody rb;


//    //Start is called once
//    private void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        gravityMultiplier = 10;
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        //Add extra gravity
//        //Physics.gravity = new Vector3(0, gravityAmount * gravityMultiplier, 0);
//        //Physics.gravity = Vector3.down * Time.deltaTime * gravityMultiplier;
//        rb.AddForce(Vector3.down * Time.deltaTime * gravityMultiplier);

//        if (Input.GetAxis("Mouse ScrollWheel") > 0f && gravityMultiplier <= 10)
//        {
//            gravityMultiplier += gravityIncrement;  
//        }
//        if (Input.GetAxis("Mouse ScrollWheel") < 0f && gravityMultiplier >= 0)
//        {
//            gravityMultiplier -= gravityIncrement;
//        }

//        Boundaries();
//    }

//    //Stops gravity from going outside of desired range
//    private void Boundaries()
//    {
//        if (gravityMultiplier > 10)
//        {
//            gravityMultiplier = 10;
//        }
//        if (gravityMultiplier < 0)
//        {
//            gravityMultiplier = 0;
//        }
//    }
//}
