using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;

    public PlayerController(PlayerModel playerModel, PlayerView playerView)
    {
        this.playerModel = playerModel;
        this.playerView = playerView;

        playerView.shootCooldown = playerModel.shootCooldown;

        playerModel.shoot += playerView.StarFireCooldown;
        playerModel.shoot += playerView.FireSound;

        playerModel.isMoving += playerView.MoveSound;

        playerModel.onDeath += playerView.DeathSound;

        playerModel.targetHit += playerView.StopFireCooldown;
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        LookAt();
        Move();
        Shoot();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerModel.ChangeLineal();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerModel.ChangeSinusoidal();
        }
    }

    private void Move()
    {
        playerModel.Move(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void LookAt()
    {
        playerModel.LookAt(Input.mousePosition);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerModel.Shoot();
        }
    }
}
