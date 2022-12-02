using Sandbox;

namespace DoubleJump
{
    // WIP
    public partial class DoubleJumpProxy : APawnProxy
    {
        [Net, Predicted]
        public bool AlreadyDoubleJumped { get; set; }

        public override void Simulate(Client cl)
        {
            if (Pawn is not Player pawn) return;

            var onGround = pawn.GroundEntity?.IsValid() == true;
            if (onGround) AlreadyDoubleJumped = false;

            base.Simulate(cl);

            if (!onGround && !AlreadyDoubleJumped && Input.Pressed(InputButton.Jump))
            {
                AlreadyDoubleJumped = true;
                pawn.Velocity = pawn.Velocity.WithZ(300);
            }
        }
    }
}