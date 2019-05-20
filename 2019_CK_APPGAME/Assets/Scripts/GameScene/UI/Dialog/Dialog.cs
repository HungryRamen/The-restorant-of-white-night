﻿using SheetData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogCommand;

namespace GameScene
{
    public class Dialog : MonoBehaviour
    {
        public Text[] dialogTextUI;

        private Queue<DlgCmd> textQueue;

        private bool bTextFullLoad;

        private float elapsedTextTime;

        private void Awake()
        {
            bTextFullLoad = true;
            dialogTextUI = GetComponentsInChildren<Text>();
            elapsedTextTime = 0f;
            
        }

        private void Start()
        {
            NextText();
        }

        private void Update()
        {
            DialogOutput();
        }

        private void DialogOutput()
        {
            if (bTextFullLoad && UIMgr.GetUIMgr().ScreenReaction())
            {
                NextText();
            }
            else if (!bTextFullLoad)
            {
                TextOutput(UIMgr.GetUIMgr().ScreenReaction());
            }
            dialogTextUI[0].text = UIMgr.GetUIMgr().textStringBuilder.ToString();
        }

        private void NextText()
        {
            bTextFullLoad = false;
            TextStackType textListQueue = UIMgr.GetUIMgr().NextText();
            dialogTextUI[0].text = UIMgr.GetUIMgr().textStringBuilder.ToString();
            dialogTextUI[1].text = textListQueue.textTypeList[textListQueue.TextTypeIndex].TalkerName;
            textQueue = new Queue<DlgCmd>(textListQueue.textTypeList[textListQueue.TextTypeIndex++].textQueue);
            UIMgr.GetUIMgr().NextTextCount();
            TextDequeue();
        }

        private void PassText()
        {
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(true);
            }
            elapsedTextTime = 0f;
            UIMgr.GetUIMgr().bChAppend = false;
            bTextFullLoad = true;
        }

        private void TextOutput(bool bPass)
        {
            if (bPass)
            {
                PassText();
                return;
            }
            elapsedTextTime += Time.deltaTime;
            if (!(UIMgr.GetUIMgr().textOutputTime <= elapsedTextTime) && UIMgr.GetUIMgr().bChAppend)
            {
                return;
            }
            TextDequeue();
            if (textQueue.Count == 0)
            {
                bTextFullLoad = true;
            }
        }

        private void TextDequeue()
        {
            elapsedTextTime = 0f;
            UIMgr.GetUIMgr().bChAppend = false;
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(false);
                if (UIMgr.GetUIMgr().bChAppend)
                {
                    break;
                }
            }
        }

        private void VoiceOutput(string voice)
        {
        }
    }
}