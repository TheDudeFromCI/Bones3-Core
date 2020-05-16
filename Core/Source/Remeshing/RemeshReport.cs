using System.Collections.Generic;

namespace Bones3Rebuilt
{
    /// <summary>
    /// The output data from a chunk remesh task.
    /// </summary>
    public class RemeshReport
    {
        /// <summary>
        /// Gets the position of the chunk that was targeted.
        /// </summary>
        /// <value>The chunk position.</value>
        public ChunkPosition ChunkPosition { get; }

        /// <summary>
        /// Gets the collision mesh that was generated.
        /// </summary>
        /// <value>The collision mesh.</value>
        public LayeredProcMesh CollisionMesh { get; }

        /// <summary>
        /// Gets the visual mesh that was generated.
        /// </summary>
        /// <value>The visual mesh.</value>
        public LayeredProcMesh VisualMesh { get; }

        /// <summary>
        /// Gets the array of texture atlases that were referenced in this remesh task,
        /// corresponding to the layers within the visual mesh.
        /// </summary>
        /// <value>The texture atlas for each layer of the visual mesh.</value>
        public ITextureAtlas[] Atlases { get; }

        /// <summary>
        /// Creates a new remesh report.
        /// </summary>
        /// <param name="chunkPosition">The position of the chunk that was targeted.</param>
        /// <param name="collisionMesh">The new collision mesh.</param>
        /// <param name="visualMesh">The new visual mesh.</param>
        /// <param name="atlases">The array of referenced texture atlases.</param>
        public RemeshReport(ChunkPosition chunkPosition, LayeredProcMesh collisionMesh, LayeredProcMesh visualMesh, ITextureAtlas[] atlases)
        {
            ChunkPosition = chunkPosition;
            CollisionMesh = collisionMesh;
            VisualMesh = visualMesh;
            Atlases = atlases;
        }
    }
}