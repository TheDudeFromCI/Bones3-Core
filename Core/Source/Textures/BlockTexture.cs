namespace Bones3Rebuilt
{
    /// <summary>
    /// A block texture is a game texture which can be applied to a block face.
    /// </summary>
    public class BlockTexture : IBlockTexture
    {
        /// <inheritdoc cref="IBlockTexture"/>
        public ITextureAtlas Atlas { get; }

        /// <inheritdoc cref="IBlockTexture"/>
        public int Index { get; }

        /// <summary>
        /// Creates a new standard block texture.
        /// </summary>
        /// <param name="atlas">The atlas which owns this block texture.</param>
        /// <param name="index">The index of this texture within the atlas.</param>
        public BlockTexture(ITextureAtlas atlas, int index)
        {
            Atlas = atlas;
            Index = index;
        }
    }
}
