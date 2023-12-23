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

    void Update()
    {
        // Joystick verilerini al
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Hareket vektörünü oluþtur
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (GameManager.Instance.gameStat == GameManager.GameStat.start && !joystick.gameObject.activeInHierarchy)
            joystick.gameObject.SetActive(true);
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && movement.magnitude >= 0.1f && mainAnim != AnimStat.hit && !CharacterManager.Instance.CharacterFight().GetIsHit())
        {
            animController.CallRunAnim();
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else if (GameManager.Instance.gameStat == GameManager.GameStat.start && mainAnim != AnimStat.hit && !CharacterManager.Instance.CharacterFight().GetIsHit()  )
            animController.CallIdleAnim();


        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetAnimStat(AnimStat animStat)
    {
        mainAnim = animStat;
        if (animStat == AnimStat.hit)
            animController.CallHitAnim();
    }
}
