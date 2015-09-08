using UnityEngine;
using System.Collections;

public interface Interactable
{
    void OnPress(MyTouch touch);
    void OnRelease(MyTouch touch);
    void OnHold(MyTouch touch);
    void OnMove(MyTouch touch);
}
