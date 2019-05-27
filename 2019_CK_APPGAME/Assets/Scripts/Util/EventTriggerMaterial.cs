﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameScene;

namespace Util
{
    public class EventTriggerMaterial : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public Image[] mainSprite;
        public FoodMaterialButtonMgr foodMgr;
        public ImageDrag imageDrag;
        public bool onCheck = false;
        private void Awake()
        {
            mainSprite = GetComponentsInChildren<Image>();
            foodMgr = GetComponent<FoodMaterialButtonMgr>();
            imageDrag = GameObject.FindWithTag("Drag").GetComponent<ImageDrag>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (foodMgr.GetState() == ESpriteState.Enable) //우클릭 && 현재상태
            {
                imageDrag.ChangeImage(mainSprite[1].sprite);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            imageDrag.OffImage();
            UIMgr.GetUIMgr().DragImagCheck(name, mainSprite[1].sprite);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ButtonSoundMgr.BtState.setValue(0);
            ButtonSoundMgr.SoundOn();
            onCheck = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonSoundMgr.BtState.setValue(1);
            ButtonSoundMgr.SoundOn();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right && foodMgr.GetState() == ESpriteState.Enable && onCheck) //우클릭 && 현재상태
            {
                ButtonSoundMgr.BtState.setValue(2);
                ButtonSoundMgr.SoundOn();
                UIMgr.GetUIMgr().MaterialRightClickSelect(name, mainSprite[1].sprite);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onCheck = false;
        }

    }
}