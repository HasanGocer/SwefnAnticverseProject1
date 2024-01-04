using UnityEngine;

public class JoystickInput : MonoSingleton<JoystickInput>
{
    public enum AnimStat
    {
        idle,
        run,
        hit
    }

    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    [SerializeField] DynamicJoystick joystick;
    [SerializeField] AnimController animController;
    private AnimStat mainAnim = AnimStat.idle;
    [SerializeField] Rigidbody rb;
    bool isIdle = false;

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (GameManager.Instance.gameStat == GameManager.GameStat.start && !joystick.gameObject.activeInHierarchy)
            joystick.gameObject.SetActive(true);
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && movement.magnitude >= 0.1f && mainAnim != AnimStat.hit && !CharacterManager.Instance.CharacterFight().GetIsHit())
        {
            animController.CallRunAnim();
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            isIdle = false;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else if (GameManager.Instance.gameStat == GameManager.GameStat.start && mainAnim != AnimStat.hit && !CharacterManager.Instance.CharacterFight().GetIsHit())
        {
            if (!isIdle)
                if (animController.GetHitAnimBool())
                    animController.SetRunBool(false);
                else
                    animController.CallIdleAnim();
            isIdle = true;
            rb.velocity = Vector3.zero;
        }


        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetIsIdle(bool tempIsIdle)
    {
        isIdle = tempIsIdle;
    }
    public void SetAnimStat(AnimStat animStat)
    {
        mainAnim = animStat;
        if (animStat == AnimStat.hit)
            animController.CallHitAnim();
    }
}
