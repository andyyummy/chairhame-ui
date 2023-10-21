using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballController : MonoBehaviour
{

    // 在編輯器中用於調整的公開變量
    public Transform Ball;                        // 對籃球的參考
    public Transform PosDribble;                  // 玩家相對位置的運球位置
    public Transform PosOverHead;                 // 抬頭時的球位置（當抬起球時）
    public Transform Arms;                        // 對玩家手臂的參考
    public Transform Target;                      // 投籃的目標

    // 私有狀態變量
    private bool IsBallInHands = true;            // 球是否目前在手中
    private bool IsBallFlying = false;            // 球是否在半空中（正在投擲）
    private bool IsDribbling = false;             // 玩家是否正在運球
    private float T = 0;                          // 用於投擲球的時間計數器
    private Vector3 initialBallPosition;          // 球的初始位置（用於重置）

    void Start()
    {
        initialBallPosition = Ball.position;     // 儲存球的初始位置
    }

    // 每一幀都會被調用
    void Update()
    {

        // 如果球在手中
        if (IsBallInHands)
        {

            // 抬頭持球（準備投擲）
            if (Input.GetKey(KeyCode.Space))
            {
                Ball.position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 360;
                // transform.LookAt(Target.parent.position);  // 玩家朝向目標
            }

            // 運球動作
            else if (Input.GetKey(KeyCode.R))
            {
                IsDribbling = true;
                // 使球上下震動以模擬運球
                Ball.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                Arms.localEulerAngles = Vector3.right * 0;
            }

            // 停止運球動作
            else if (Input.GetKeyUp(KeyCode.R))
            {
                IsDribbling = false;
            }

            // 投擲球
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                IsBallInHands = false;
                IsBallFlying = true;
                T = 0;
            }
        }

        // 球在空中的移動邏輯（被投出後）
        if (IsBallFlying)
        {
            T += Time.deltaTime;
            float duration = 0.66f;
            float t01 = T / duration;

            // 對球的移動進行線性插值，使其朝向目標
            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // 為球的投擲增加拋物線運動
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            // 球到達目標時的邏輯
            if (t01 >= 1)
            {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(ResetBallPosition());  // 2秒後重置球的位置
            }
        }
    }

    // 檢查玩家是否靠近球以拾起它
    private void OnTriggerEnter(Collider other)
    {
        if (!IsBallInHands && !IsBallFlying)
        {
            IsBallInHands = true;
            Ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // 協程來重置球的位置
    IEnumerator ResetBallPosition()
    {
        yield return new WaitForSeconds(2);   // 等待2秒
        Ball.position = initialBallPosition; // 將球重置到初始位置
        Ball.GetComponent<Rigidbody>().isKinematic = true; // 使球變為運動學的，這樣它不會因物理原因而掉下
        IsBallInHands = true;
    }
}