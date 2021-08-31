using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;

    internal void SetPushing(bool state)
    {
        anim.SetBool("isPushing", state);
    }

    internal void GetHit()
    {
        anim.SetTrigger("GetHit");
    }
}
