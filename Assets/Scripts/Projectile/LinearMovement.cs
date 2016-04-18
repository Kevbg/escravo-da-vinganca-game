using UnityEngine;

public class LinearMovement: IMovement
{
	public void move(GameObject gameObject, float direction) {
		gameObject.transform.Translate (Time.deltaTime * (50) * direction, 0f, 0f);
	}
}