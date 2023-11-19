using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace oepncvwithunity
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private Button _imageFlipButton;
        [SerializeField] private Image _targetImage;

#if UNITY_ANDROID && !UNITY_EDITOR
        private const string _dllName = "unityOpenCV";

        [DllImport(_dllName)]
        private static extern void FlipImage(IntPtr target, int width, int height);
#else
        private const string _dllName = "OpenCVwithUnityAndroid";

        [DllImport(_dllName)]
        private static extern void FlipImage(ref Color32[] target, int width, int height);
#endif

        private void Start()
        {
            _imageFlipButton.onClick.AddListener(OnImageFlipButton);
        }


        private void OnImageFlipButton()
        {
            var targetImagePixels = _targetImage.sprite.texture.GetPixels32();
            Debug.Log($"before enter to process : {targetImagePixels.Length}");
#if UNITY_ANDROID && !UNITY_EDITOR
            // 1���� �迭�� IntPtr�� ��ȯ�Ͽ� ����
            GCHandle handle = GCHandle.Alloc(targetImagePixels, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();

            FlipImage(ptr, _targetImage.sprite.texture.width, _targetImage.sprite.texture.height);

            // ����
            handle.Free();
#else
            FlipImage(ref targetImagePixels, _targetImage.sprite.texture.width, _targetImage.sprite.texture.height);
#endif
            Debug.Log($"after enter to process : {targetImagePixels.Length}");
            _targetImage.sprite.texture.SetPixels32(targetImagePixels);
            _targetImage.sprite.texture.Apply();
        }
    }
}
