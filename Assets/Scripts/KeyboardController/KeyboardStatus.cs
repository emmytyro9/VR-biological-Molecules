﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace CurvedVRKeyboard
{

    [SelectionBase]
    public class KeyboardStatus : KeyboardComponent
    {

        //-----------SET IN UNITY --------------
        [SerializeField]
        public string output;
        public static string sentOutput;
        [SerializeField]
        public int maxOutputLength;
        [SerializeField]
        public GameObject targetGameObject;
        //CameraController cam;

        //----CurrentKeysStatus----
        [SerializeField]
        public Component typeHolder;
        [SerializeField]
        public bool isReflectionPossible;
        private KeyboardItem[] keys;
        private bool areLettersActive = true;
        private bool isLowercase = true;
        private const char BLANKSPACE = ' ';  // ==>> This is where the spacebar change to enter button. **Change the behavior of spacebar to enter.
        private const string TEXT = "text";
        private Component textComponent;


        /// <summary>
        /// Handles click on keyboarditem
        /// </summary>
        /// <param name="clicked">keyboard item clicked</param>
        public void HandleClick(KeyboardItem clicked)
        {
            string value = clicked.GetValue();
            if (value.Equals(QEH) || value.Equals(ABC))
            { // special signs pressed
                ChangeSpecialLetters();
            }
            else if (value.Equals(UP) || value.Equals(LOW))
            { // upper/lower case pressed
                LowerUpperKeys();
            }
            else if (value.Equals(SPACE))
            {
                // TypeKey(BLANKSPACE);
                /* ==>> This is where the events occured after hit enter button.
                        1. Receieve the value of text.
                        2. Make the event occures.
                            2.1 User should be leaded to another scene.
                            2.2 Or to the spacific coordinate.
                */

                print("Debug : Inside handleClick(KeyboardStatus)"); //Debugging
                //sentOutput = GetOutput();
                if(GetOutput() == "egfr")
                {
                    print("Debug : Inside if condition(KeyboardStatus)"); //Debugging
                    //SceneManager.LoadScene("egfr");
                    SceneManager.LoadScene("egfr");
                }
               
            }
            else if (value.Equals(BACK))
            {
                BackspaceKey();
            }
            else
            {// Normal letter
                TypeKey(value[0]);
            }
        }

        /// <summary>
        /// Displays special signs
        /// </summary>
        private void ChangeSpecialLetters()
        {
            KeyLetterEnum ToDisplay = areLettersActive ? KeyLetterEnum.NonLetters : KeyLetterEnum.LowerCase;
            areLettersActive = !areLettersActive;
            isLowercase = true;
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].SetKeyText(ToDisplay);
            }
        }

        /// <summary>
        /// Changes between lower and upper keys
        /// </summary>
        private void LowerUpperKeys()
        {
            KeyLetterEnum ToDisplay = isLowercase ? KeyLetterEnum.UpperCase : KeyLetterEnum.LowerCase;
            isLowercase = !isLowercase;
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].SetKeyText(ToDisplay);
            }
        }

        private void BackspaceKey()
        {
            if (output.Length >= 1)
            {
                textComponent = targetGameObject.GetComponent(typeHolder.GetType());
                textComponent.GetType().GetProperty(TEXT).SetValue(textComponent, output.Remove(output.Length - 1, 1), null);
                output = output.Remove(output.Length - 1, 1);
            }
        }

        private void TypeKey(char key)
        {
            if (output.Length < maxOutputLength)
            {
                textComponent = targetGameObject.GetComponent(typeHolder.GetType());
                textComponent.GetType().GetProperty(TEXT).SetValue(textComponent, output + key.ToString(), null);
                output = output + key.ToString();
            }

        }

        public void SetKeys(KeyboardItem[] keys)
        {
            this.keys = keys;
        }

        public void setOutput(ref string stringRef)
        {
            output = stringRef;
        }

        public string GetOutput()
        {
            return output.ToString();
        }
    }
}
