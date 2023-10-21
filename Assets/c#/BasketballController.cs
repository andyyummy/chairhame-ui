using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballController : MonoBehaviour {

    public float MoveSpeed = 4;
    public Transform Ball;
    public Transform PosDribble;
    public Transform PosOverHead;
    public Transform Arms;
    public Transform Target;

    // variables
    private bool IsBallInHands = true;
    private bool IsBallFlying = false;
    private bool IsDribbling = false; // Added this for dribbling state
    private float T = 0;
    private Vector3 initialBallPosition;

    void Start() {
        initialBallPosition = Ball.position;
    }

    // Update is called once per frame
    void Update() {

        // walking
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + direction);

        // ball in hands
        if (IsBallInHands) {

            // hold over head
            if (Input.GetKey(KeyCode.Space)) {
                Ball.position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 180;

                // look towards the target
                transform.LookAt(Target.parent.position);
            }

            // dribbling by pressing 'R'
            else if (Input.GetKey(KeyCode.R)) {
                IsDribbling = true;
                Ball.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                Arms.localEulerAngles = Vector3.right * 0;
            }

            // stop dribbling when 'R' is released
            else if (Input.GetKeyUp(KeyCode.R)) {
                IsDribbling = false;
            }

            // throw ball
            else if (Input.GetKeyUp(KeyCode.Space)) {
                IsBallInHands = false;
                IsBallFlying = true;
                T = 0;
            }

        }

        // ball in the air
        if (IsBallFlying) {
            T += Time.deltaTime;
            float duration = 0.66f;
            float t01 = T / duration;

            // move to target
            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            // moment when ball arrives at the target
            if (t01 >= 1) {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(ResetBallPosition());
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!IsBallInHands && !IsBallFlying) {
            IsBallInHands = true;
            Ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator ResetBallPosition() {
        yield return new WaitForSeconds(2); // wait for 2 seconds
        Ball.position = initialBallPosition;
        Ball.GetComponent<Rigidbody>().isKinematic = true; // Make it kinematic so it doesn't fall
        IsBallInHands = true;
    }
}