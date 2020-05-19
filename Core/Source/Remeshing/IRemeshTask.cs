namespace Bones3Rebuilt.Remeshing
{
    /// <summary>
    /// A task which was executed to generate a single mesh layer of a chunk
    /// based on the given properties of that chunk.
    /// </summary>
    public interface IRemeshTask
    {
        /// <summary>
        /// Waits for this task to finish executing before continuing.
        /// </summary>
        /// <returns>The generated mesh.</returns>
        ProcMesh Finish();
    }
}
