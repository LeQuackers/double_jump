using Sandbox;

namespace DoubleJump
{
    // WIP
    [Spawnable]
    [Library("ent_doublejumpitem", Group = "entities", Title = "Double Jump Item")]
    public partial class DoubleJumpItem : Prop, IUse
    {
        public override void Spawn()
        {
            base.Spawn();

            SetModel("models/sbox_props/wooden_crate/wooden_crate.vmdl");
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);

            Tags.Add("prop", "solid");
        }



        public bool IsUsable(Entity user) => true;

        public bool OnUse(Entity user)
        {
            var cl = user.Client;
            if (cl != null)
            {
                new DoubleJumpProxy().AttachToClient(cl);
                Delete();
            }

            return false;
        }
    }
}