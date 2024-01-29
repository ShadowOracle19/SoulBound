using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveAnim : MonoBehaviour
{
    public Animator animator;
    public bool move;
    
    public void PressButton()
    {
        move = !move;
        animator.SetBool("CameraMove", move);
    }
}
