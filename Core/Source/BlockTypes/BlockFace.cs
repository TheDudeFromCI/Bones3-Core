namespace Bones3Rebuilt
{
    /// <summary>
    /// The properties for a single face of a block type.
    /// </summary>
    public class BlockFace
    {
        /// <summary>
        /// The side of the block this face is on.
        /// </summary>
        /// <value>The block side index.</value>
        public int Side { get; }

        /// <summary>
        /// The rotation value for this face's texture.
        /// </summary>
        /// <value>The rotation value of this face.</value>
        public FaceRotation Rotation { get; }

        /// <summary>
        /// Gets the block texture which is applied to this block face.
        /// </summary>
        /// <value>The texture, or null if this block face has no texture.</value>
        public IBlockTexture Texture { get; }

        /// <summary>
        /// Creates a new block face.
        /// </summary>
        /// <param name="side">The side of the block this face is on.</param>
        /// <param name="rotation">The texture rotation for this block face.</param>
        /// <param name="texture">The texture for this block face.</param>
        internal BlockFace(int side, FaceRotation rotation, IBlockTexture texture)
        {
            Side = side;
            Rotation = rotation;
            Texture = texture;
        }
    }
}
