using UnityEngine;
using UnityEditor;

public class TexturePostProcessor : AssetPostprocessor
{
    bool runscript = false;

    void OnPostprocessTexture(Texture texture)
    {
        if (runscript)
        {
            TextureImporter importer = assetImporter as TextureImporter;
            importer.textureType = TextureImporterType.Image;
            importer.anisoLevel = 16;
            importer.filterMode = FilterMode.Bilinear;

            Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture));
            if (asset)
            {
                EditorUtility.SetDirty(asset);
            }
            else
            {
                texture.anisoLevel = 16;
                texture.filterMode = FilterMode.Bilinear;
            }
        }
    }
}
