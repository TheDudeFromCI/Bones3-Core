using Bones3Rebuilt.DataTypes;

namespace Bones3Rebuilt.BlockTypes
{
    /// <summary>
    /// A builder for creating block types.
    /// </summary>
    public class BlockBuilder
    {
        private readonly FaceRotation[] m_FaceRotations = new FaceRotation[6];
        private readonly IBlockTexture[] m_FaceTextures = new IBlockTexture[6];
        private readonly ushort m_ID;
        private string m_Name = "New Block";
        private bool m_Solid = true;
        private bool m_Visible = true;

        /// <summary>
        /// Initializes the builder for creating a block with the given ID.
        /// </summary>
        /// <param name="id">The block ID.</param>
        public BlockBuilder(ushort id)
        {
            m_ID = id;
        }

        /// <summary>
        /// Sets the name of the block type.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>This builder.</returns>
        public BlockBuilder Name(string name)
        {
            m_Name = name;
            return this;
        }

        /// <summary>
        /// Sets whether or not the block is solid.
        /// </summary>
        /// <param name="solid">The solidity.</param>
        /// <returns>This builder.</returns>
        public BlockBuilder Solid(bool solid)
        {
            m_Solid = solid;
            return this;
        }

        /// <summary>
        /// Sets whether or not the block is visible.
        /// </summary>
        /// <param name="solid">The visibility.</param>
        /// <returns>This builder.</returns>
        public BlockBuilder Visible(bool visible)
        {
            m_Visible = visible;
            return this;
        }

        /// <summary>
        /// Sets the texture rotation for a given block face.
        /// </summary>
        /// <param name="face">The face index.</param>
        /// <param name="rotation">The texture rotation.</param>
        /// <returns>This builder.</returns>
        public BlockBuilder FaceRotation(int face, FaceRotation rotation)
        {
            m_FaceRotations[face] = rotation;
            return this;
        }

        /// <summary>
        /// Sets the texture to use for the given block face.
        /// </summary>
        /// <param name="face">The face index.</param>
        /// <param name="texture">The texture.</param>
        /// <returns>This builder.</returns>
        public BlockBuilder Texture(int face, IBlockTexture texture)
        {
            m_FaceTextures[face] = texture;
            return this;
        }

        /// <summary>
        /// Creates a new block type based on the current state of this builder.
        /// </summary>
        /// <returns>The new block type.</returns>
        public BlockType Build()
        {
            var faces = new BlockFace[6];
            for (int i = 0; i < faces.Length; i++)
                faces[i] = new BlockFace(i, m_FaceRotations[i], m_FaceTextures[i]);

            return new BlockType(m_ID, m_Name, m_Solid, m_Visible, faces);
        }
    }
}
