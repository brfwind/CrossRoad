using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;//引用Input输入的方法

public class PlayerController : MonoBehaviour
{
    private enum Direction
    {
        Up, Right, Left
    }
    private Direction dir;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private PlayerInput playerInput;
    private BoxCollider2D coll;

    [Header("得分")]
    public int stepPoint;
    private int pointResult;

    [Header("跳跃")]
    public float jumpDistance;
    private float moveDistance;
    private Vector2 destination;
    private Vector2 touchPosition;
    private bool isJump;
    private bool canJump;
    private bool buttonHeld;//记录按钮被长按
    private bool isBackGround;
    private bool isDead;
    private RaycastHit2D[] result = new RaycastHit2D[2];

    #region 周期函数
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        rb.position = Vector2.Lerp(transform.position, destination, 0.134f);
    }

    private void Update()
    {
        if (isDead)
        {
            DisableInput();
            return;
        }

        if (canJump)
            {
                TriggerJump();
                canJump = false;
            }
    }

    #endregion

    #region 碰撞箱Trigger判断
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water") && !isJump)
        {
            Physics2D.RaycastNonAlloc(transform.position + Vector3.up * 0.1f, Vector2.down, result);

            bool inWater = true;

            foreach (var hit in result)
            {
                if (hit.collider == null)
                    continue;

                if (hit.collider.CompareTag("Wood"))
                {
                    inWater = false;
                    transform.parent = hit.collider.transform;
                }

                Collider2D wood = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("WoodLayer"));

                if (wood != null)
                {
                    inWater = false;
                }
            }

            if (inWater && !isJump)
            {
                Debug.Log("In Water GAME OVER!");
                isDead = true;
            }
        }

        if (other.CompareTag("Border") || other.CompareTag("Car"))
        {
            Debug.Log("GAME OVER!");
            isDead = true;
        }

        if (!isJump && other.CompareTag("Obstacle"))
        {
            Debug.Log("GAME OVER!");
            isDead = true;
        }

        if (isDead)
        {
            EventHandler.CallGameOverEvent();
            coll.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BackGround"))
        {
            isBackGround = true;
            sr.sortingOrder = 0;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BackGround"))
        {
            isBackGround = false;
        }
    }

    #endregion

    #region 按键事件
    public void Jump(InputAction.CallbackContext context)
    {
        //TODO:播放跳跃音效
        if (context.performed && !isJump)
        {
            // Debug.Log("JUMP!" + " " + moveDistance);//会在控制台打印
            moveDistance = jumpDistance;
            canJump = true;

            AudioManager.instance.SetJumpClip(0);
        }

        if (dir == Direction.Up && context.performed && !isJump)
        {
            pointResult += stepPoint;
        }
    }

    public void LongJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = jumpDistance * 2;
            buttonHeld = true;

            AudioManager.instance.SetJumpClip(1);
        }

        if (context.canceled && buttonHeld && !isJump)
        {
            // Debug.Log("LONG JUMP!" + " " + moveDistance);
            if (dir == Direction.Up)
                pointResult += stepPoint * 2;

            buttonHeld = false;
            canJump = true;
        }

    }

    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            // Debug.Log(touchPosition);
            var offset = ((Vector3)touchPosition - new Vector3(0, transform.position.y, transform.position.z)).normalized;

            if (Mathf.Abs(offset.x) <= 0.6f)
            {
                dir = Direction.Up;
            }
            else if (offset.x < 0)
            {
                dir = Direction.Left;
            }
            else if (offset.x > 0)
            {
                dir = Direction.Right;
            }
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            dir = Direction.Left;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            dir = Direction.Right;
        }
    }

    public void MoveUp(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            dir = Direction.Up;
        }
    }

    #endregion

    /// <summary>
    /// 触发执行跳跃动画
    /// </summary>
    private void TriggerJump()
    {
        anim.SetTrigger("Jump");
        switch (dir)
        {
            case Direction.Up:
                anim.SetBool("isSide", false);
                destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
                transform.localScale = Vector3.one;
                break;
            case Direction.Left:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x - moveDistance, transform.position.y);
                transform.localScale = Vector3.one;
                break;
            case Direction.Right:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x + moveDistance, transform.position.y);
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }

    #region 动画事件
    public void JumpAnimationEvent()
    {
        AudioManager.instance.PlayJumpFx();

        isJump = true;

        if (!isBackGround)
        {
            sr.sortingOrder = 1;
        }

        transform.parent = null;
    }

    public void FinishJumpAnimationEvent()
    {
        isJump = false;
        sr.sortingOrder = 0;

        if (dir == Direction.Up && !isDead)
        {
            EventHandler.CallGetPointEvent(pointResult);
        }
    }

    #endregion

    private void DisableInput()
    {
        playerInput.enabled = false;
    }
}
