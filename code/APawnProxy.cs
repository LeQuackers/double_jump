using Sandbox;
using System;

namespace DoubleJump
{
    // WIP
    public abstract partial class APawnProxy : Entity
    {
        public Entity Pawn => Parent;



        public override void Spawn()
        {
            base.Spawn();

            Transmit = TransmitType.Owner;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (!Client.IsValid() || !Parent.IsValid()) return;

            Client.Pawn = Parent;
        }



        public virtual void AttachToClient(Client cl)
        {
            var pawn = cl.Pawn;
            if (!pawn.IsValid()) return;

            pawn.Owner = this;
            cl.Pawn = this;

            Parent = pawn;
            LocalPosition = Vector3.Zero;
            LocalRotation = Rotation.Identity;
            Components.Add(new ProxyCamera());
        }



        public override void Simulate(Client cl)
        {
            base.Simulate(cl);
            Parent?.Simulate(cl);
        }

        public override void FrameSimulate(Client cl)
        {
            base.FrameSimulate(cl);
            Parent?.FrameSimulate(cl);
        }



        public override void TakeDamage(DamageInfo info)
        {
            base.TakeDamage(info);
            Parent?.TakeDamage(info);
        }



        public override void PostCameraSetup(ref CameraSetup setup)
        {
            base.PostCameraSetup(ref setup);
            Parent?.PostCameraSetup(ref setup);
        }



        public override void BuildInput(InputBuilder input)
        {
            base.BuildInput(input);
            Parent?.BuildInput(input);
        }



        public override void OnChildAdded(Entity child)
        {
            base.OnChildAdded(child);
            Parent?.OnChildAdded(child);
        }

        public override void OnChildRemoved(Entity child)
        {
            base.OnChildRemoved(child);
            Parent?.OnChildRemoved(child);
        }
    }



    // WIP
    public readonly struct SkipPawnProxy : IDisposable
    {
        public readonly Entity Proxy;
        public readonly Entity Pawn;

        public SkipPawnProxy()
        {
            var client = Local.Client;
            var proxy = Proxy = client.Pawn;
            client.Pawn = Pawn = proxy.Parent;
        }

        public void Dispose()
        {
            Local.Client.Pawn = Proxy;
        }
    }



    // WIP
    public class ProxyCamera : CameraMode
    {
        public override void Activated()
        {
            using var skipPawnProxy = new SkipPawnProxy();
            skipPawnProxy.Pawn.Components.Get<CameraMode>()?.Activated();
        }

        public override void Deactivated()
        {
            using var skipPawnProxy = new SkipPawnProxy();
            skipPawnProxy.Pawn.Components.Get<CameraMode>()?.Deactivated();
        }

        public override void Build(ref CameraSetup camSetup)
        {
            using var skipPawnProxy = new SkipPawnProxy();
            skipPawnProxy.Pawn.Components.Get<CameraMode>()?.Build(ref camSetup);
        }

        public override void BuildInput(InputBuilder input)
        {
            using var skipPawnProxy = new SkipPawnProxy();
            skipPawnProxy.Pawn.Components.Get<CameraMode>()?.BuildInput(input);
        }

        public override void Update()
        {
            using var skipPawnProxy = new SkipPawnProxy();
            skipPawnProxy.Pawn.Components.Get<CameraMode>()?.Update();
        }
    }
}