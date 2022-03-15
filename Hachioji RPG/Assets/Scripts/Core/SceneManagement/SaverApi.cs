using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SaverApi : MonoBehaviour
    {
        const string SaveFile = "savefile";
        SavingSystem saver;
        [SerializeField] float FadeInTime = 0.5f;
        // Update is called once per frame

        IEnumerator Start()
        {
            saver = GetComponent<SavingSystem>();
            Fader fader = FindObjectOfType<Fader>();
            fader.instantFadeOut();
            yield return saver.LoadLastScene(SaveFile);
            yield return fader.FadeIn(FadeInTime);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Delete();
            }
        }

        public void Load()
        {
            saver.Load(SaveFile);
        }

        public void Save()
        {
            saver.Save(SaveFile);
        }

        public void Delete()
        {
            saver.Delete(SaveFile);
        }
    }
}
