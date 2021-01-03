using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;
    private Vector2Int playerDirection;
    private Vector2Int moveDirection;

    private void Awake() => playerController = GameManager.PlayerController;

    private void Update()
    {
        if (GameManager.GameMode)
        {
            #region CharacterDirection
            Vector2 mouseDirection = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerController.gameObject.transform.position;
            float angle;
            if (mouseDirection.y >= 0)
                angle = Mathf.RoundToInt(Vector2.Angle(Vector2.right, mouseDirection));
            else
                angle = Mathf.RoundToInt(Vector2.Angle(Vector2.left, mouseDirection)) + 180;

            float turnedAngle = angle - 22.5f;
            if (turnedAngle < 0) turnedAngle += 360;
            int sectorIndex = (int)turnedAngle / 45;

            switch (sectorIndex)
            {
                case 0:
                    playerDirection = new Vector2Int(1, 1);
                    break;
                case 1:
                    playerDirection = new Vector2Int(0, 1);
                    break;
                case 2:
                    playerDirection = new Vector2Int(-1, 1);
                    break;
                case 3:
                    playerDirection = new Vector2Int(-1, 0);
                    break;
                case 4:
                    playerDirection = new Vector2Int(-1, -1);
                    break;
                case 5:
                    playerDirection = new Vector2Int(0, -1);
                    break;
                case 6:
                    playerDirection = new Vector2Int(1, -1);
                    break;
                case 7:
                    playerDirection = new Vector2Int(1, 0);
                    break;
            }
            playerController.SetCharacterDirection(playerDirection);
            #endregion
            #region MoveDirection
            moveDirection = Vector2Int.zero;
            if (Input.GetKey(KeyCode.W)) moveDirection += Vector2Int.up;
            if (Input.GetKey(KeyCode.S)) moveDirection += Vector2Int.down;
            if (Input.GetKey(KeyCode.A)) moveDirection += Vector2Int.left;
            if (Input.GetKey(KeyCode.D)) moveDirection += Vector2Int.right;
            playerController.SetMoveDirection(moveDirection);
            #endregion
            #region Escape
            if (Input.GetKeyDown(KeyCode.Space)) playerController.EscapeAbility();
            #endregion
            #region Attack
            if (Input.GetMouseButton(0))
            {
                Vector2 attackDirection = mouseDirection;
                if (attackDirection == Vector2.zero)
                    attackDirection = playerDirection;

                playerController.Attack(attackDirection.normalized);
            }
            #endregion
        }
    }
}
