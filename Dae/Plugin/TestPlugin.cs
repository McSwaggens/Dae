using System;
using static Dae.Util.PositionAnchor;

namespace Dae.Plugin
{
	[DCustomComponent (name = "Button")]
	internal class Button : Component
	{
		public Button ( IVector size ) : base (size)
		{
		}

		public override void OnParentSizeChanged ( Canvas parent, IVector parentNewSize )
		{
		}

		public override void Render ()
		{
			buffer.Blank ();
			string str = "Size: " + Size;
			IVector s = AnchorSize (new IVector (str.Length, 1), Size, AnchorX.Middle, AnchorY.Bottom);
			buffer.DrawFrame (focused ? Color.blue : Color.red, 'i');
			buffer.Write (str, Color.white, Color.black, s);
		}

		public override void OnMouseEnter ( IVector localPosition )
		{
			base.OnMouseEnter (localPosition);
			SignalRender ();
		}

		public override void OnMouseLeave ()
		{
			base.OnMouseLeave ();
			SignalRender ();
		}
	}

	[DCustomComponent (name = "Foo")]
	internal class Foo : Canvas
	{
		public Foo ( IVector size ) : base (size)
		{
		}

		public override void OnParentSizeChanged ( Canvas parent, IVector parentNewSize )
		{
			ChangeSize (parentNewSize.Half);
			SetPosition (parent.Size / 2, AnchorX.Middle, AnchorY.Middle);
		}

		public override void Render ()
		{
			buffer.Blank ();
			base.Render ();
			buffer.DrawFrame (focused ? Color.blue : Color.red, '|');
		}

		public override void OnMouseEnter ( IVector localPosition )
		{
			base.OnMouseEnter (localPosition);
			SignalRender ();
		}

		public override void OnMouseLeave ()
		{
			base.OnMouseLeave ();
			SignalRender ();
		}
	}

	[DPluginInformation (name = "TestPlugin", author = "Daniel", description = "A simple plugin for testing our DAE's plugin system")]
	internal class TestPlugin : DPlugin
	{
		public override bool Load ()
		{
			Logger.Log ("Hello World from the TestPlugin Loader function!");

			return true; // Ran successfully
		}

		public override void Unload ()
		{
		}

		public void HelloWorld ()
		{
			Logger.Log ("Hello World!");
		}
	}
}