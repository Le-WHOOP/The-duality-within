using UnityEditor;
using UnityEngine;

public class SpriteImporter : AssetPostprocessor {

    /// <summary>
    /// Set the default values for 2d sprites.
    /// </summary>
    /// <remarks>
    /// These will be applied automatically on new Texture2D import.
    /// </remarks>
    /// <param name="texture">The imported sprite.</param>
    void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spritePixelsPerUnit = 32;
        textureImporter.filterMode = FilterMode.Point;

        textureImporter.SetPlatformTextureSettings(new TextureImporterPlatformSettings {
            maxTextureSize = 2048,
            resizeAlgorithm = TextureResizeAlgorithm.Bilinear,
            textureCompression = TextureImporterCompression.Uncompressed
        });
    }
}
