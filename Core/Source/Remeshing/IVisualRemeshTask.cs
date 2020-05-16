namespace Bones3Rebuilt
{
    /// <summary>
    /// An extension of remesh tasks which specifically target visual mesh generation.
    /// </summary>
    public interface IVisualRemeshTask : IRemeshTask
    {
        /// <summary>
        /// Gets the texture atlas this remesh task is targeting.
        /// </summary>
        /// <value>The texture atlas.</value>
        ITextureAtlas Atlas { get; }
    }
}