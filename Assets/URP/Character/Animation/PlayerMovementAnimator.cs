using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementAnimator : MonoBehaviour
{
    public Animator anim;
    float speed;

    void Update()
    {
        float h = 0f;
        float v = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) v += 1f;
            if (Keyboard.current.sKey.isPressed) v -= 1f;
            if (Keyboard.current.dKey.isPressed) h += 1f;
            if (Keyboard.current.aKey.isPressed) h -= 1f;
        }

        float input = new Vector2(h, v).magnitude;

        speed = Mathf.Lerp(speed, input * 2f, Time.deltaTime * 10f);
        anim.SetFloat("Speed", speed);
    }
}
