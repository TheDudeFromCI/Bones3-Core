namespace Bones3Rebuilt.Remeshing
{
    // TODO Add locking mechanisms to ensure thread safety to Reset() and SetBlock() methods.

    /// <summary>
    /// Provides a method of storing chunk properties for remeshing.
    /// </summary>
    public class ChunkProperties
    {
        private IMeshBlockDetails[] m_Blocks = new IMeshBlockDetails[0];

        /// <summary>
        /// Gets the size of the chunk being handled.
        /// </summary>
        /// <value>The chunk size.</value>
        public GridSize ChunkSize { get; private set; }

        /// <summary>
        /// Gets the position of the chunk.
        /// </summary>
        /// <value>The chunk position.</value>
        public ChunkPosition ChunkPosition { get; private set; }

        /// <summary>
        /// Prepares this chunk properties for a chunk with the given size and position. This
        /// should only be called by the main thread when tasks are not actively using it.
        /// </summary>
        /// <param name="chunkPos">The chunk position.</param>
        /// <param name="chunkSize">The chunk size.</param>
        /// <remarks>
        /// If the chunk size is less than the allocated memory size, then a new array is allocated.
        /// If analyzing many chunks of different sizes, it minimizes garbage creation by scanning
        /// the largest chunk first.
        /// </remarks>
        public void Reset(ChunkPosition chunkPos, GridSize chunkSize)
        {
            ChunkPosition = chunkPos;
            ChunkSize = chunkSize;

            int blockCount = chunkSize.Volume;
            blockCount += chunkSize.Value * chunkSize.Value * 6; // Neighbor chunks

            if (m_Blocks.Length < blockCount)
                m_Blocks = new IMeshBlockDetails[blockCount];
        }

        /// <summary>
        /// Sets a block type at the position within this chunk. The block may be up to
        /// one block outside of the chunk bounds. This should only be called from the
        /// main thread, and should not be called when this chunk properties object is
        /// being used by the remesh tasks.
        /// </summary>
        /// <param name="pos">The position of the block.</param>
        /// <returns>The block type.</returns>
        public void SetBlock(BlockPosition pos, IMeshBlockDetails details)
        {
            m_Blocks[BlockIndex(pos)] = details;
        }

        /// <summary>
        /// Gets a block type at the position within this chunk. The block may be up to
        /// one block outside of the chunk bounds.
        /// </summary>
        /// <param name="pos">The position of the block.</param>
        /// <returns>The block type.</returns>
        public IMeshBlockDetails GetBlock(BlockPosition pos)
        {
            return m_Blocks[BlockIndex(pos)];
        }

        /// <summary>
        /// Gets the block list index of the given block position.
        /// </summary>
        /// <param name="pos">The block position.</param>
        /// <returns>The block index.</returns>
        private int BlockIndex(BlockPosition pos)
        {
            int j = GetChunkSide(pos);

            if (j == -1)
                return pos.Index(ChunkSize);

            return GetNextBlock(pos, j);
        }

        /// <summary>
        /// Gets the side of the chunk the block position is.
        /// </summary>
        /// <param name="pos">The block position.</param>
        /// <returns>The chunk side, or -1 if the position is within the chunk.</returns>
        private int GetChunkSide(BlockPosition pos)
        {
            if (pos.X < 0)
                return 0;

            if (pos.X >= ChunkSize.Value)
                return 1;

            if (pos.Y < 0)
                return 2;

            if (pos.Y >= ChunkSize.Value)
                return 3;

            if (pos.Z < 0)
                return 4;

            if (pos.Z >= ChunkSize.Value)
                return 5;

            return -1;
        }

        /// <summary>
        /// Gets the index of the neighbor block based on the given block position and chunk edge.
        /// </summary>
        /// <param name="pos">The block position.</param>
        /// <param name="j">The chunk edge.</param>
        /// <returns>The block index.</returns>
        private int GetNextBlock(BlockPosition pos, int j)
        {
            switch (j)
            {
                case 0:
                case 1:
                    pos = new BlockPosition(j, pos.Y, pos.Z);
                    break;

                case 2:
                case 3:
                    pos = new BlockPosition(j, pos.X, pos.Z);
                    break;

                case 4:
                case 5:
                    pos = new BlockPosition(j, pos.X, pos.Y);
                    break;
            }

            return pos.Index(ChunkSize) + ChunkSize.Volume;
        }
    }
}
