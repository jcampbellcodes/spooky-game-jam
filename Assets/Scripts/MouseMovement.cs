using UnityEngine;
using System.Collections;

public class MouseMovement : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        

    }
    void OnMouseDrag()
    {
        //float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
