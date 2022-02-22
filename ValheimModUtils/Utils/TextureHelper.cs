using UnityEngine;

namespace MilkWyzardStudios.Valheim.Utils
{
    public static class TextureHelper
    {
        public static Texture2D BlendTextures(Texture2D tex1, Texture2D tex2)
        {
            Color[] pixelArray1 = tex1.GetPixels();
            Color[] pixelArray2 = tex2.GetPixels();

            int width = Mathf.Max(tex1.width, tex2.width);
            int height = Mathf.Max(tex1.height, tex2.height);
            int totalLen = Mathf.Max(pixelArray1.Length, pixelArray2.Length);
            Color[] finalColor = new Color[totalLen];

            for (int i = 0; i < totalLen; i++)
            {
                Color baseColor = i < pixelArray1.Length ? pixelArray1[i] : new Color(0.0f, 0.0f, 0.0f, 0.0f);
                Color layerColor = i < pixelArray2.Length ? pixelArray2[i] : new Color(0.0f, 0.0f, 0.0f, 0.0f);

                finalColor[i] = Color.Lerp(baseColor, layerColor, layerColor.a);
            }

            Texture2D texture = new Texture2D(width, height);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;
            texture.SetPixels(finalColor);
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// Takes a texture (marked as isReadable = false) and makes a readable copy.
        /// https://support.unity.com/hc/en-us/articles/206486626-How-can-I-get-pixels-from-unreadable-textures-
        /// </summary>
        /// <param name="texture">The source texture (not readable)</param>
        /// <param name="textureRect">Region of the texture to copy.</param>
        /// <returns>A copy of the pixel data (within the given region) as a new texture, marked readable.</returns>
        public static Texture2D CreateReadableClone(Texture2D texture, Rect textureRect)
        {
            Texture2D copyTexture = new Texture2D((int)textureRect.width, (int)textureRect.height);
            copyTexture.wrapMode = TextureWrapMode.Clamp;
            copyTexture.filterMode = FilterMode.Point;

            // make a copy of just the portion of the texture we wish to capture
            Graphics.CopyTexture(texture, 0, 0, (int)textureRect.x, (int)textureRect.y, (int)textureRect.width, (int)textureRect.height, copyTexture, 0, 0, 0, 0);

            // Create a temporary RenderTexture of the same size as the texture
            RenderTexture tmp = RenderTexture.GetTemporary(
                                copyTexture.width,
                                copyTexture.height,
                                0,
                                RenderTextureFormat.Default,
                                RenderTextureReadWrite.Linear);
            tmp.depth = 32;
            tmp.enableRandomWrite = true;
            //RenderTexture tmp = new RenderTexture(copyTexture.width, copyTexture.height, 32);
            //tmp.name = "IconRenderTexture";
            //tmp.enableRandomWrite = true;
            //tmp.Create();

            // Blit the pixels on texture to the RenderTexture
            Graphics.Blit(copyTexture, tmp);

            // Backup the currently set RenderTexture
            RenderTexture previous = RenderTexture.active;

            // Set the current RenderTexture to the temporary one we created
            RenderTexture.active = tmp;

            // Create a new readable Texture2D to copy the pixels to it
            Texture2D myTexture2D = new Texture2D(copyTexture.width, copyTexture.height, TextureFormat.RGBA32, false, true);

            // Copy the pixels from the RenderTexture to the new Texture
            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();

            // Perform gamma correction
            var pixels = myTexture2D.GetPixels();
            GammaCorrection(ref pixels);
            myTexture2D.SetPixels(pixels);
            myTexture2D.Apply();

            // Reset the active RenderTexture
            RenderTexture.active = previous;

            // Release the temporary RenderTexture
            RenderTexture.ReleaseTemporary(tmp);
            //tmp.Release();
            //UnityEngine.Object.Destroy(tmp);

            return myTexture2D;
        }

        public static void GammaCorrection(ref Color[] pixelData)
        {
            for (int i = 0; i < pixelData.Length; i++)
            {
                var pixel = pixelData[i];
                pixelData[i] = new Color(
                    Mathf.Pow(pixel.r, 1f / 2.2f),
                    Mathf.Pow(pixel.g, 1f / 2.2f),
                    Mathf.Pow(pixel.b, 1f / 2.2f),
                    Mathf.Pow(pixel.a, 1f / 2.2f));
            }
        }
    }
}
