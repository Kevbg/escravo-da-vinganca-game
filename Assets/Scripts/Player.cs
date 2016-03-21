using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public const float DefaultSpeed = 30;
    public float moveSpeed;

    private static float xAxis;
    private Vector2 newPosition;
    private bool isGrounded;

    void Start () {
        newPosition = transform.position;
        moveSpeed = DefaultSpeed;
    }

	void Update () {
        checkInput();
	}

    void checkInput() {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (xAxis != 0) {
            move(moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            GetComponent<Rigidbody2D>().AddForce(transform.up * 900);
        }
    }

    void move(float speed = DefaultSpeed) {
        newPosition = new Vector2(transform.position.x + xAxis, transform.position.y);
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * speed);
    }

    public static float getDirection() {
        return xAxis;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Ground") {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.name == "Ground") {
            isGrounded = false;
        }
    }
}
