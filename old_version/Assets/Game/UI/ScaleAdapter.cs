using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ScaleAdapter : MonoBehaviour
    {
        RectTransform rectTransform;
        float sign = 1f;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        // Start is called before the first frame update
        void Start()
        {
            sign = Mathf.Sign(rectTransform.anchoredPosition.x);
        }

        // Update is called once per frame
        void Update()
        {
            float height = rectTransform.rect.height;            
            rectTransform.sizeDelta = new Vector2(height, rectTransform.sizeDelta.y);
            rectTransform.anchoredPosition = new Vector2(sign * height / 2, rectTransform.anchoredPosition.y);
        }
    }
}

