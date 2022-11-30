using UnityEngine;

public class GravitySphere : GravitySource
{
	[SerializeField]
	float gravity = 9.81f;

	[SerializeField, Min(0f)]
	float outerRadius = 10f, outerFalloffRadius = 15f;

	[SerializeField, Min(0f)]
	float innerFalloffRadius = 1f, innerRadius = 5f;

	float innerFalloffFactor, outerFalloffFactor;

	public override Vector3 GetGravity(Vector3 position)
	{
		Vector3 difference = transform.position - position;
		
		float distance = difference.magnitude;
		if(distance > outerFalloffRadius || distance < innerFalloffRadius)
		{
			return Vector3.zero;
		}

		float strength = gravity / distance;
		if(distance > outerRadius)
		{
			strength *= 1f - (distance - outerRadius) * outerFalloffFactor;
		}
		else if(distance < innerRadius)
		{
			strength *= 1f - (innerRadius - distance) * innerFalloffFactor;
		}

		return difference * strength;
	}

	private void Awake()
	{
		OnValidate();
	}

	private void OnValidate()
	{
		innerFalloffRadius = Mathf.Max(innerFalloffRadius, 0f);
		innerRadius = Mathf.Max(innerRadius, innerFalloffRadius);
		outerRadius = Mathf.Max(outerRadius, innerRadius);
		outerFalloffRadius = Mathf.Max(outerFalloffRadius, outerRadius);

		innerFalloffFactor = 1f / (innerRadius - innerFalloffRadius);
		outerFalloffFactor = 1f / (outerFalloffRadius - outerRadius);
	}

	private void OnDrawGizmos()
	{
		Vector3 position = transform.position;

		if(innerFalloffRadius > 0f && innerFalloffRadius < innerRadius)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(position, innerFalloffRadius);
		}

		if(innerRadius > 0f && innerRadius < outerRadius)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(position, innerRadius);
		}

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(position, outerRadius);
		
		if(outerFalloffRadius > outerRadius)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(position, outerFalloffRadius);
		}
	}
}
