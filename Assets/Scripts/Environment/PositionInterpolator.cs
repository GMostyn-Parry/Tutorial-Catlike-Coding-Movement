using UnityEngine;

public class PositionInterpolator : MonoBehaviour
{
	[SerializeField]
	Rigidbody body = default;

	[SerializeField]
	Vector3 from = default, to = default;

	[SerializeField]
	Transform relativeTo = default;

	public void Interpolate(float t)
	{
		Vector3 position;
		if(relativeTo)
		{
			position = Vector3.LerpUnclamped(relativeTo.TransformPoint(from), relativeTo.TransformPoint(to), t);
		}
		else
		{
			position = Vector3.LerpUnclamped(from, to, t);
		}

		body.MovePosition(position);
	}
}
