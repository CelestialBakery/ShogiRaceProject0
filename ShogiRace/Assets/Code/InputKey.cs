using UnityEngine;
using UnityEngine.InputSystem;

public class InputKey : MonoBehaviour
{
    public UnityEngine.InputSystem.Controls.KeyControl
        WKey,
        SKey,
        AKey,
        DKey,
        LeftShiftKey,
        SpaceKey;

    private void Update()
    {
        // 現在のマウス情報
        //var mCurrent = Mouse.current;
        var kCurrent = Keyboard.current;

        // マウス接続チェック
        //if (mCurrent == null)
        //{
        //    // マウスが接続されていないと
        //    // Mouse.currentがnullになる
        //    return;
        //}
        if (kCurrent == null)
        {
            return;
        }

        // マウスカーソル位置取得
        //var cursorPosition = mCurrent.position.ReadValue();

        // 左ボタンの入力状態取得
        //var leftButton = mCurrent.leftButton;

        //var wKey = kCurrent.wKey;
        //var sKey = kCurrent.sKey;
        //var aKey = kCurrent.aKey;
        //var dKey = kCurrent.dKey;
        //var spaceKey = kCurrent.spaceKey;

        WKey = kCurrent.wKey;
        SKey = kCurrent.sKey;
        AKey = kCurrent.aKey;
        DKey = kCurrent.dKey;
        LeftShiftKey = kCurrent.leftShiftKey;
        SpaceKey = kCurrent.spaceKey;

        /*
        // 左ボタンが押された瞬間かどうか
        if (leftButton.wasPressedThisFrame)
        {
            Debug.Log($"左ボタンが押された！ {cursorPosition}");
        }

        // 左ボタンが離された瞬間かどうか
        if (leftButton.wasReleasedThisFrame)
        {
            Debug.Log($"左ボタンが離された！{cursorPosition}");
        }

        // 左ボタンが押されているかどうか
        if (leftButton.isPressed)
        {
            Debug.Log($"左ボタンが押されている！{cursorPosition}");
        }
        */
    }
}
