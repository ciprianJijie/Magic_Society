using UnityEngine;

public class RandomRotation : MonoBehaviour
{
	[RangeAttribute(1, 12)]
	public int Steps;

	[RangeAttribute(0, 360)]
    public float StepAngle;

    public Vector3 Axis;

    protected void Start()
	{
        int randomSteps;

        randomSteps = Random.Range(0, Steps);

        this.gameObject.transform.Rotate(Axis, randomSteps * StepAngle);
	}

}
