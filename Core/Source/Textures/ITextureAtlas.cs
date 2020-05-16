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
        /// Creates a new block texture and adds it to this atlas.
        /// </summary>
        /// <returns>The newly created texture.</returns>
        IBlockTexture AddTexture();

        /// <summary>
        /// Gets the texture within this atlas at the specified index.
        /// </summary>
        /// <param name="index">The texture index.</param>
        /// <returns>The block texture.</returns>
        IBlockTexture GetTexture(int index);
    }
}
