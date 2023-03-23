using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Player Basic
    //按鍵設定
    [Header("玩家按鍵")]
    public PlayerKeyCode playerKeyCodes;//玩家按鍵

    [Header("移動速度")]
    [SerializeField] float speed;//移動速度
    float[] moveItem = { 0, 0 };//移動數值

    [Header("跳躍")]
    bool isJump;//是否跳躍
    Rigidbody rigi;
    [SerializeField] float Jumpspeed;//跳躍速度

    #endregion

    #region RunVar
    //奔跑處理
    private float lastClickTime;//最後點擊時間
    private float doubleTapTimeThreshold = 0.3f;//0.3秒內按兩次則奔跑
    private bool isRunning;//是否奔跑
    KeyCode tempCode;//判斷按的按鍵與上一次按的是否相同

    #endregion
    //

    public static playerController playerController_;//唯一性


    private void Awake()
    {
        playerController_ = this;
        rigi = GetComponent<Rigidbody>();
        initKecode();
    }

    void initKecode()
    {
        KeyCode[] keys = keyCodes();
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i] == KeyCode.None)
            {
                switch (i)
                {
                    case 0:
                        playerKeyCodes.leftMove = KeyCode.A;
                        break;
                    case 1:
                        playerKeyCodes.rightMove = KeyCode.D;
                        break;
                    case 2:
                        playerKeyCodes.frontMove = KeyCode.W;
                        break;
                    case 3:
                        playerKeyCodes.BackMove = KeyCode.S;
                        break;
                    case 4:
                        playerKeyCodes.Jump = KeyCode.K;
                        break;
                }
            }
        }
    }

    public KeyCode[] keyCodes()
    {
        KeyCode[] keys = { playerKeyCodes.leftMove, playerKeyCodes.rightMove, playerKeyCodes.frontMove, playerKeyCodes.BackMove, playerKeyCodes.Jump };
        return keys;
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            if (rigi.velocity.y == 0)
            {
                rigi.AddForce(Vector3.up * Jumpspeed, ForceMode.Impulse);
            }
            isJump = false;
        }

        rigi.MovePosition(rigi.position + new Vector3(moveItem[0] * speed * Time.fixedDeltaTime, 0, moveItem[1] * speed * 2 * Time.fixedDeltaTime) * (isRunning ? 1.3f : 1.0f));

    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        bool isRunx = true;
        bool isRunY = true;

        if (Input.GetKeyDown(playerKeyCodes.Jump))
        {
            isJump = true;

        }

        if (Input.GetKey(playerKeyCodes.leftMove))
        {
            moveItem[0] = -1;
        }
        else if (Input.GetKey(playerKeyCodes.rightMove))
        {
            moveItem[0] = 1;
        }
        else
        {
            moveItem[0] = 0;

            isRunx = false;
        }

        if (Input.GetKey(playerKeyCodes.frontMove))
        {

            moveItem[1] = 1;
        }
        else if (Input.GetKey(playerKeyCodes.BackMove))
        {
            moveItem[1] = -1;
        }
        else
        {
            moveItem[1] = 0;

            isRunY = false;
        }

        Run();

        if (isRunx && isRunY)
        {
            moveItem[0] /= 2;
            moveItem[1] /= 1.1f;
        }



    }

    void Run()
    {
        bool isWalk = true;
        KeyCode nowKecode = KeyCode.None;

        if (Input.GetKeyDown(playerKeyCodes.leftMove))
        {
            nowKecode = playerKeyCodes.leftMove;
        }
        else if (Input.GetKeyDown(playerKeyCodes.rightMove))
        {
            nowKecode = playerKeyCodes.leftMove;
        }
        else if (Input.GetKeyDown(playerKeyCodes.frontMove))
        {
            nowKecode = playerKeyCodes.frontMove;
        }
        else if (Input.GetKeyDown(playerKeyCodes.BackMove))
        {
            nowKecode = playerKeyCodes.BackMove;
        }
        else
        {
            isWalk = false;
        }

        if (isWalk)
        {
            if (Time.time - lastClickTime < doubleTapTimeThreshold && nowKecode == tempCode)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            lastClickTime = Time.time;

            tempCode = nowKecode;
        }

    }

}


[System.Serializable]
public class PlayerKeyCode
{
    public KeyCode leftMove;
    public KeyCode rightMove;
    public KeyCode frontMove;
    public KeyCode BackMove;
    public KeyCode Jump;
    public KeyCode OpenShop;
    public KeyCode OpenBag;
}
