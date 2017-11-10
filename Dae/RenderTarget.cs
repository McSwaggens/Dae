﻿using OpenTK.Graphics.OpenGL4;
using System;

namespace Dae
{
	public class RenderTarget : IEnable, IDisposable, DObject
	{
		public bool isStatic = false;
		private int frameBuffer = -1;
		internal int colorBuffer = -1, depthBuffer = -1;

		internal static RenderTarget defaultRenderTarget = new RenderTarget (0, true);

		public RenderTarget (int frameBuffer, bool isStatic = false)
		{
			Dae.Register (this);
			this.isStatic = isStatic;
			this.frameBuffer = frameBuffer;
		}

		public RenderTarget (DWindow window, bool isStatic = false)
		{
			Dae.Register (this);
			GenerateFrameBuffer (new Vector (window.Width, window.Height));
			this.isStatic = isStatic;
		}

		public RenderTarget (Vector size, bool isStatic = false)
		{
			Dae.Register (this);
			GenerateFrameBuffer (size);
			this.isStatic = isStatic;
		}

		private void GenerateFrameBuffer (Vector size)
		{
			if (isStatic)
			{
				return;
			}

			if (frameBuffer == -1)
			{
				frameBuffer = GL.GenFramebuffer ();
				GL.BindFramebuffer (FramebufferTarget.Framebuffer, frameBuffer);

				colorBuffer = GL.GenTexture ();
				GL.BindTexture (TextureTarget.Texture2D, colorBuffer);
				GL.TexImage2D (TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)size.x, (int)size.y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, (IntPtr)0);
				GL.TexParameter (TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
				GL.TexParameter (TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
				GL.GenerateMipmap (GenerateMipmapTarget.Texture2D);

				GL.FramebufferTexture2D (FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, colorBuffer, 0);

				depthBuffer = GL.GenRenderbuffer ();
				GL.BindRenderbuffer (RenderbufferTarget.Renderbuffer, depthBuffer);
				GL.RenderbufferStorage (RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, (int)size.x, (int)size.y);

				GL.FramebufferRenderbuffer (FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, depthBuffer);

				FramebufferErrorCode code = GL.CheckFramebufferStatus (FramebufferTarget.Framebuffer);

				if (code != FramebufferErrorCode.FramebufferComplete || code != FramebufferErrorCode.FramebufferCompleteExt)
				{
					Logger.Print ("Error: RenderTarget was unable to initialize");
					Logger.Print ("\t FramebufferErrorCode: " + code);
				}

				Logger.Print ("RenderTarget initialize successfully");

				// Bind the default framebuffer
				GL.BindFramebuffer (FramebufferTarget.Framebuffer, 0);
			}
		}

		public void Regenerate (DWindow window)
		{
			Dispose ();
			GenerateFrameBuffer (new Vector (window.Width, window.Height));
		}

		public void Dispose ()
		{
			GL.DeleteFramebuffer (frameBuffer);
			frameBuffer = -1;
		}

		public void Enable ()
		{
			GL.BindFramebuffer (FramebufferTarget.Framebuffer, frameBuffer);
		}
	}
}