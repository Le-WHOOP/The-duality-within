using UnityEditor;
using UnityEngine;

public class SpriteImporter : AssetPostprocessor {

    void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spritePixelsPerUnit = 32;
        textureImporter.filterMode = FilterMode.Point;

        textureImporter.SetPlatformTextureSettings( new TextureImporterPlatformSettings {
            maxTextureSize = 2048,
            resizeAlgorithm = TextureResizeAlgorithm.Bilinear,
            textureCompression = TextureImporterCompression.Uncompressed
        });
    }
}
