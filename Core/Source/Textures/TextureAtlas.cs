using System.Collections.Generic;

namespace Bones3Rebuilt
{
    /// <summary>
    /// A texture atlas is a container of textures which can be applied to block faces.
    /// </summary>
    public class TextureAtlas : ITextureAtlas
    {
        private readonly List<IBlockTexture> m_TextureList = new List<IBlockTexture>();

        /// <inheritdoc cref="ITextureAtlas"/>
        public int Count => m_TextureList.Count;

        /// <inheritdoc cref="ITextureAtlas"/>
        public void AddTexture(IBlockTexture texture) => m_TextureList.Add(texture);

        /// <inheritdoc cref="ITextureAtlas"/>
        public IBlockTexture GetTexture(int index) => m_TextureList[index];
    }
}
