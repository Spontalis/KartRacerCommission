﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    /// <summary>
    /// A basic keyboard implementation of the IInput interface for all the input information a kart needs.
    /// </summary>
    public class KeyboardInput : MonoBehaviour, IInput
    {
        public float Acceleration
        {
            get { return m_Acceleration; }
        }
        public float Steering
        {
            get { return m_Steering; }
        }
        public bool HopPressed
        {
            get { return m_HopPressed; }
        }
        public bool HopHeld
        {
            get { return m_HopHeld; }
        }

        public float m_Acceleration;
        public float m_Steering;
        bool m_HopPressed;
        bool m_HopHeld;
      
        bool m_FixedUpdateHappened;

        //bools to prevent conflicting movements
        public bool forward;
        public bool backward;
        public bool leftward;
        public bool rightward;
        //bools to set which control scheme to follow
        bool mobileControlActive;
        bool computerControlsActive;

        private void Start()
        {
            forward = false;
            backward = false;
            rightward = false;
            leftward = false;

            //setting the control scheme used by taking it from the controllerPlatformHandler script
            mobileControlActive = ControllerPlatformHandler.instance.touchControls;
            computerControlsActive = ControllerPlatformHandler.instance.computerControls;
        }
        void Update ()
        {           
            if (computerControlsActive)
            {
                if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                    m_Acceleration = 1f;
                else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
                    m_Acceleration = -1f;
                else
                    m_Acceleration = 0f;

                if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    m_Steering = -1f;
                else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
                    m_Steering = 1f;
                else
                    m_Steering = 0f;
            }

            //Touch Controls for Steering
            if (mobileControlActive)
            {

                if (forward && !backward)
                    m_Acceleration = 1f;
                else if (!forward && backward)
                    m_Acceleration = -1f;
                else
                    m_Acceleration = 0f;


                if (leftward && !rightward)
                    m_Steering = -1f;
                else if (!leftward && rightward)
                    m_Steering = 1f;
                else
                    m_Steering = 0f;
            }
          
            if (m_FixedUpdateHappened)
            {
                m_FixedUpdateHappened = false;
            }
        }

        void FixedUpdate ()
        {
            m_FixedUpdateHappened = true;
        }


        //movement functions accessed by touch controls
        public void Accelerating()
        {
            forward = true;
        }

        public void Reversing()
        {
            backward = true;        
        }

        public void GoingLeft()
        {
            leftward = true;
        }

        public void GoingRight()
        {
            rightward = true;
        }
    }
}