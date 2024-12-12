using UnityEngine;

public class BowHolder : MonoBehaviour
{
    private void Update(){
        JoystickAngle();
    }
    private void JoystickAngle(){
        Vector2 dir = GameSetting.Instance.joystickDir;
        if (dir != Vector2.zero) 
        {
            float scx = Mathf.Sign(PlayerManager.Instance.currentPlayer.transform.localScale.x);
            float angle = Mathf.Atan2(dir.y * scx, dir.x * scx) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle); // Xoay đối tượng trực tiếp
        }
    }
}
