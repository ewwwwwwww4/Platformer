using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float horizontalMove;  // 用于存储水平移动方向的变量
    public float speed = 2f;  // 控制角色移动速度的公共变量

    Rigidbody2D myBody;  // 用于存储角色刚体组件的引用

    bool grounded = false;  // 检测角色是否着地的布尔变量

    public float castDist = 1f;  // 地面检测射线的距离
    public float jumpPower = 2f;  // 控制角色跳跃力量的公共变量
    public float gravityScale = 5f;  // 控制角色下落速度的公共变量
    public float gravityFall = 40f;  // 控制角色下落速度的公共变量

    bool jump = false;  // 检测是否进行跳跃的布尔变量
    bool doubleJump; 

    Animator myAnim;  // 用于存储角色动画组件的引用

    // 在第一帧之前调用的方法
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();  // 获取角色的刚体组件引用
        myAnim = GetComponent<Animator>();  // 获取角色的动画组件引用
    }

    // 在每一帧中调用的方法
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");  // 获取水平输入

        if (grounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
        // 如果按下跳跃键并且角色着地
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || doubleJump)
            {
                myAnim.SetBool("jumping", true);  // 设置动画状态为跳跃
                jump = true;  // 触发跳跃
                doubleJump = !doubleJump;
            }
        }

        // 如果水平移动大于0.2或小于-0.2
        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            myAnim.SetBool("running", true);  // 设置动画状态为奔跑
        }
        else
        {
            myAnim.SetBool("running", false);  // 设置动画状态为非奔跑
        }
    }

    // 在固定的时间间隔内调用的方法
    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;  // 计算角色的水平移动速度

        // 如果需要跳跃
        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);  // 以冲量方式添加向上的力以执行跳跃
            jump = false;  // 取消跳跃状态
        }

        // 根据角色的垂直速度设置重力规模
        if (myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;  // 设置上升时的重力规模
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;  // 设置下落时的重力规模
        }

        // 发射一条射线检测是否着地
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);  // 在场景中绘制射线以进行调试

        // 如果射线检测到地面
        if (hit.collider != null && hit.transform.name == "Ground")
        {
            myAnim.SetBool("jumping", false);  // 设置动画状态为非跳跃
            grounded = true;  // 角色着地
        }
        else
        {
            grounded = false;  // 角色不着地
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);  // 更新角色的速度
    }
}
