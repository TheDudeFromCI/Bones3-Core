namespace Bones3Rebuilt.Remeshing
{
    /// <summary>
    /// An extension of remesh tasks which specifically target visual mesh generation.
    /// </summary>
    public interface IVisualRemeshTask : IRemeshTask
    {
        /// <summary>
        /// Gets the material ID this remesh task is targeting.
        /// </summary>
        /// <value>The material ID.</value>
        int MaterialID { get; }
    }
}
