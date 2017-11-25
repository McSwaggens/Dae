using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Dae
{
	public struct Texture : IDisposable, IGLType
	{
		public static List<Texture> allTextures = new List<Texture> ();

		internal static void ClearAll ()
		{
			allTextures.ForEach (m => m.Dispose ());
			allTextures.Clear ();
		}

		public int textureId;
		public readonly Vector size;
		public readonly Vector scale;

		public Texture ( int textureId )
		{
			this.textureId = textureId;
			size = Vector.zero;
			scale = Vector.one;
		}

		public Texture ( Bitmap image, TextureFilter filter = TextureFilter.None, bool reverseTexture = true )
		{
			// OpenGL Y coordinate is flipped (0 is at the bottom, 1 is the top)
			// Flip the image upside-down, this avoids useless y flipping in shaders too!
			if (reverseTexture)
			{
				image.RotateFlip (RotateFlipType.Rotate180FlipX);
			}

			// Generate a texture id to use
			textureId = GL.GenTexture ();

			// Bind our texture so we can use it
			GL.BindTexture (TextureTarget.Texture2D, textureId);

			// Get a BitmapData object, this allows us to get a pointer to the pixel data (OpenGL required)
			System.Drawing.Imaging.BitmapData data = image.LockBits (
					new Rectangle (0, 0, image.Width, image.Height),
					System.Drawing.Imaging.ImageLockMode.ReadOnly,
					System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			// Use Linear filtering, make sure this is used, otherwise we wont see our image
			GL.TexParameter (TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filter);
			GL.TexParameter (TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)filter);

			// Upload our image to the GPU
			GL.TexImage2D (TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			// Unlock the Bitmap
			image.UnlockBits (data);

			size = new Vector (data.Width, data.Height);
			float xScale = size.x / size.y;
			float yScale = size.y / size.x;

			if (xScale == yScale)
			{
				scale = Vector.one;
			}

			scale = new Vector (xScale <= 1 ? xScale : 1, yScale <= 1 ? yScale : 1);
			Logger.Log ("Scale " + scale);

			allTextures.Add (this);
		}

		public object Clone ()
		{
			return new Texture (textureId);
		}

		public void Dispose ()
		{
			// Delete our texture from the GPU
			GL.DeleteTexture (textureId);
		}

		public void UniformUpload ( int locationId )
		{
			GL.ActiveTexture (TextureUnit.Texture0 + Material.textureStack);
			GL.BindTexture (TextureTarget.Texture2D, textureId);
		}
	}
}