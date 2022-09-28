using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Movement : Abilities
    {
        [SerializeField] float accelerationValue = 2f;
        //[SerializeField] float deaccelerationValue = 2f;
        [SerializeField] float rotationValue = 2f;
        [SerializeField] float forceValue = 2f;


        protected virtual void Update()
        {
            //MoveByCoordinateTranslation();
            //MoveByRigidBodyPhysics();
            moveUsingInBuildTransformTranslateMethod();
        }

        protected virtual void MoveByCoordinateTranslation()
        {
            legendaryTaxiDriver.Player.Enable();
            Vector2 moveDir = legendaryTaxiDriver.Player.Move.ReadValue<Vector2>();
            RotateCarTurnLeftOrRight(moveDir);

            if (moveDir.y != 0)
            {
                float newY = transform.position.y + moveDir.y * accelerationValue * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }

        }

        private void RotateCarTurnLeftOrRight(Vector2 moveDir)
        {
            if (moveDir.x != 0)
            {
                Quaternion rotation = transform.rotation;
                Vector3 eulerAngle = rotation.eulerAngles;
                eulerAngle.z += moveDir.x * rotationValue;
                rotation.eulerAngles = eulerAngle;
                transform.rotation = rotation;
            }
        }

        // Implemmenting roation when A or D is pressed 
        // Implementing deaccleration if nothing is pressed
        protected virtual void MoveByRigidBodyPhysics()
        {
            Vector2 moveDir = InputSystemCurrentMoveDirection();
            body.AddForce(moveDir * forceValue, ForceMode2D.Force);
            RotateCarTurnLeftOrRight(moveDir);
        }

        private Vector2 InputSystemCurrentMoveDirection()
        {
            legendaryTaxiDriver.Player.Enable();
            Vector2 moveDir = legendaryTaxiDriver.Player.Move.ReadValue<Vector2>();
            return moveDir;
        }

        protected virtual void moveUsingInBuildTransformTranslateMethod()
        {
            float move = InputSystemCurrentMoveDirection().y * accelerationValue * Time.deltaTime;
            float rotate = InputSystemCurrentMoveDirection().x * - rotationValue * Time.deltaTime;
            transform.Translate(0f, move, 0f);
            transform.Rotate(0f, 0f, rotate);
        }
    }
}

