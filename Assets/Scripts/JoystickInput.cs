using UnityEngine;

public class JoystickInput : MonoSingleton<JoystickInput>
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    [SerializeField] DynamicJoystick joystick;
    [SerializeField] Rigidbody rb;
    bool isIdle = false;

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (GameManager.Instance.gameStat == GameManager.GameStat.start && !joystick.gameObject.activeInHierarchy)
            joystick.gameObject.SetActive(true);
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && movement.magnitude >= 0.1f)
        {
            print(31);
            CharacterManager.Instance.GetAnimController().CallRunAnim();
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            isIdle = false;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            print(32);
            if (!isIdle)
                if (CharacterManager.Instance.GetAnimController().GetHitAnimBool())
                    CharacterManager.Instance.GetAnimController().SetRunBool(false);
                else
                    CharacterManager.Instance.GetAnimController().CallIdleAnim();
            isIdle = true;
            rb.velocity = Vector3.zero;
        }


        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetIsIdle(bool tempIsIdle)
    {
        isIdle = tempIsIdle;
    }
}
