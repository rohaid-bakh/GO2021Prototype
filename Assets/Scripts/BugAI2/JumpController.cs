using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
   
   public Jumper m_script;
    [SerializeField] private float m_speed;

   void Start(){

   }
   
    void FixedUpdate()
    {
       m_script.Move(Time.fixedDeltaTime * m_speed );
   }

   
}
