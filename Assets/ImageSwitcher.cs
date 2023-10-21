using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ImageSwitcher : MonoBehaviour, IPointerClickHandler
{
    public Sprite image1;  // 這是你的第一張圖片
    public Sprite image2;  // 這是你的第二張圖片
    public Sprite image3;  // 這是你的第三張圖片

    private Image imgComponent;  // 存放這個物件的 Image component

    private void Awake()
    {
        imgComponent = GetComponent<Image>();
        if (imgComponent == null)
        {
            Debug.LogError("ImageSwitcher 腳本需要附加到有 Image component 的物件上。");
        }
    }

    // 當這個物件被點擊時，這個方法會被呼叫
    public void OnPointerClick(PointerEventData eventData)
    {
        if (imgComponent.sprite == image1)
        {
            imgComponent.sprite = image2;
        }
        else if (imgComponent.sprite == image2)
        {
            imgComponent.sprite = image3;
        }
        else if (imgComponent.sprite == image3)
        {
            // 載入 "Scene_投籃" 場景
            SceneManager.LoadScene("Scene_關卡一_投籃");
        }
    }
}
