namespace Bones3Rebuilt
{
    /// <summary>
    /// A texture which can be applied to a block face. Each texture should belong to a single texture atlas.
    /// </summary>
    public interface IBlockTexture
    {
        /// <summary>
        /// Gets the texture atlas which owns this block texture.
        /// </summary>
        /// <value>The texture atlas.</value>
        ITextureAtlas Atlas { get; }

        /// <summary>
        /// Gets the index for this texture within the atlas.
        /// </summary>
        /// <value></value>
        int Index { get; }
    }
}
