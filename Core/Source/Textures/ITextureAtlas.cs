namespace Bones3Rebuilt
{
    /// <summary>
    /// A container for holding a set of textures to apply to blocks.
    /// </summary>
    public interface ITextureAtlas
    {
        /// <summary>
        /// Gets the number of textures in this atlas.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds a texture to this atlas.
        /// </summary>
        /// <param name="texture">The texture to add.</param>
        void AddTexture(IBlockTexture texture);

        /// <summary>
        /// Gets the texture within this atlas at the specified index.
        /// </summary>
        /// <param name="index">The texture index.</param>
        /// <returns>The block texture.</returns>
        IBlockTexture GetTexture(int index);
    }
}
