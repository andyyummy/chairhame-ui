using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float maxSteerAngle = 45f;
    public float motorForce = 700f;
    public float reverseForce = 500f; // 新增的后退马力

    private void Update()
    {
        float steerInput = Input.GetAxis("Horizontal");
        float throttleInput = Input.GetAxis("Vertical");

        float motorTorque = 0f;

        // 判断前进还是后退
        if (throttleInput > 0f)
        {
            motorTorque = motorForce * throttleInput;
        }
        else if (throttleInput < 0f)
        {
            motorTorque = reverseForce * throttleInput; // 使用反向马力
        }

        // 控制转向
        float steerAngle = maxSteerAngle * steerInput;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;

        // 控制马力
        rearLeftWheel.motorTorque = motorTorque;
        rearRightWheel.motorTorque = motorTorque;
    }
}
