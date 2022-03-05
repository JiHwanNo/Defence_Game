using System;
using UnityEngine;

public class MobileOptimization : Singleton<MobileOptimization>
{
    [HideInInspector] float base_width, base_height;
    [HideInInspector] float width = 3200, height = 1440;
    [HideInInspector] float ratio_w, ratio_h;


    public float Ratio_W { get { return ratio_w; } }
    public float Ratio_H { get { return ratio_h; } }
    public float Width { get { return width; } }
    public float Height { get { return height; } }

    /// <summary>
    /// 휴대폰 크기에 맞는 UI 크기를 초반에 지정해주는 함수
    /// </summary>
    public void Set_MobileValueCavas(float w, float h)
    {
        base_width = w;
        base_height = h;

        ratio_w = base_width / width;
        ratio_h = base_height / height;
    }

    public void UI_ScaleOptimization(Transform panel)
    {
        Transform[] UI = new Transform[panel.GetChild(0).childCount];
        for (int i = 0; i < UI.Length; i++)
        {
            UI[i] = panel.GetChild(0).GetChild(i);
            UI[i].localScale = new Vector2(Ratio_W, Ratio_H);
        }
    }
   
    /// <summary>
    /// obj가 메인 카메라 화면 크기에 딱 맞게 떨어지게 하기 위한 조절 함수.
    /// </summary>
    /// <param name="sr">스프라이트 랜더링 정보</param>
    /// <param name="Bg">최적화 할 오브젝트</param>
    public void Set_BgObjScale(SpriteRenderer sr, Transform Bg)
    {
        float spriteX = sr.sprite.bounds.size.x;
        float spriteY = sr.sprite.bounds.size.y;
        float screenY = Camera.main.orthographicSize * 2;
        float screenX = screenY / Screen.height * Screen.width;
        Bg.localScale = new Vector3((float)Math.Truncate((screenX / spriteX) * 1000) / 1000, (float)Math.Truncate((screenY / spriteY) * 1000) / 1000, 1f);
    }


}

