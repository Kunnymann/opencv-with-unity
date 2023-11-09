using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace oepncvwithunity
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private Button _imageFlipButton;
        [SerializeField] private Image _targetImage;

        [DllImport("OpenCVwithUnity")] private static extern void FlipImage(ref Color32[] target, int width, int height);

        private void Start()
        {
            _imageFlipButton.onClick.AddListener(OnImageFlipButton);
        }

        private void OnImageFlipButton()
        {
            var targetImagePixels = _targetImage.sprite.texture.GetPixels32();
            FlipImage(ref targetImagePixels, _targetImage.sprite.texture.width, _targetImage.sprite.texture.height);
            _targetImage.sprite.texture.SetPixels32(targetImagePixels);
            _targetImage.sprite.texture.Apply();
        }
    }
}
