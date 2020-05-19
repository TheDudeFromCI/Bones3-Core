using System.Collections.Generic;

namespace Bones3Rebuilt.BlockTypes
{
    /// <summary>
    /// A list of block properties which are used to make up a world.
    /// </summary>
    public class BlockTypeList
    {
        /// <summary>
        /// The maximum number of block types which can be added to this list.
        /// </summary>
        public const ushort MAX_BLOCK_TYPES = 65535;

        private readonly List<BlockType> m_BlockTypes = new List<BlockType>();

        /// <summary>
        /// The number of block types currently in this list.
        /// </summary>
        /// <value>The number of block types.</value>
        public int Count => m_BlockTypes.Count;

        /// <summary>
        /// Gets the next available block ID within this list.
        /// </summary>
        /// <returns>The next available block ID.</returns>
        public ushort NextBlockID => (ushort)Count;

        /// <summary>
        /// Creates a new block type list and initializes it with ungenerated and air blocks.
        /// </summary>
        public BlockTypeList()
        {
            AddBlockType(new BlockBuilder(NextBlockID)
                .Name("Ungenerated")
                .Solid(false)
                .Visible(false)
                .Build());

            AddBlockType(new BlockBuilder(NextBlockID)
                .Name("Air")
                .Solid(false)
                .Visible(false)
                .Build());
        }

        /// <summary>
        /// Gets the block type with the given ID.
        /// </summary>
        /// <param name="id">The block id.</param>
        public BlockType GetBlockType(ushort id)
        {
            return m_BlockTypes[id]; ;
        }

        /// <summary>
        /// Adds a new block type to this list.
        /// </summary>
        /// <param name="blockType">The block type to add.</param>
        /// <exception cref="System.ArgumentException">
        /// If the block count is currently at MAX_BLOCK_TYPES.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// If the block's ID does not match the NextAvailableID property
        /// of this list.
        /// </exception>
        public void AddBlockType(BlockType blockType)
        {
            if (Count == MAX_BLOCK_TYPES)
                throw new System.ArgumentException($"Too many block types! Max: {MAX_BLOCK_TYPES}");

            if (blockType.ID != NextBlockID)
                throw new System.ArgumentException($"Block ID (blockType.ID) does not match next available ID ({NextBlockID})!");

            m_BlockTypes.Add(blockType);
        }

        /// <summary>
        /// Removes a block type from this list.
        /// </summary>
        /// <param name="blockType">The block type to remove.</param>
        /// <exception cref="System.AccessViolationException">
        /// If the block type is protected.
        /// </exception>
        public void RemoveBlockType(BlockType blockType)
        {
            if (blockType == null)
                return;

            if (!m_BlockTypes.Contains(blockType))
                return;

            if (blockType.ID < 2)
                throw new System.AccessViolationException($"Cannot remove protected block type {blockType}!");

            m_BlockTypes.Remove(blockType);
        }
    }
}
