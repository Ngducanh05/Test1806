using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 1f;
    public bool canMove = false;

    private void Update()
    {
        if(canMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * speed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the tag "Ground"
        if (collision.gameObject.tag == "Ground")
        {
            canMove = true;
        }
    }
   
}
