using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
   public void ExitGameAnh()
    {
        // Nếu đang chạy trên trình biên dịch Unity, dừng trò chơi
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Nếu đang chạy trên ứng dụng thực tế, thoát ứng dụng
            Application.Quit();
#endif
    }
}
