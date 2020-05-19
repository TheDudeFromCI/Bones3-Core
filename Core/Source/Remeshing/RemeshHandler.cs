using System.Collections.Generic;

namespace Bones3Rebuilt.Remeshing
{
    /// <summary>
    /// Holds a set of remesh distrbutors in order to generate a new mesh for a chunk.
    /// </summary>
    public class RemeshHandler
    {
        private readonly List<IRemeshDistributor> m_Distributors = new List<IRemeshDistributor>();
        private readonly List<RemeshTaskStack> m_ActiveTasks = new List<RemeshTaskStack>();

        /// <summary>
        /// Analyses the given chunk and starts a set of remesh tasks for handling that chunk.
        /// </summary>
        /// <param name="properties">The chunk properties to analyze.</param>
        public void RemeshChunk(ChunkProperties properties)
        {
            var taskStack = new RemeshTaskStack(properties.ChunkPosition);
            m_ActiveTasks.Add(taskStack);

            foreach (var dis in m_Distributors)
                dis.CreateTasks(properties, taskStack);
        }

        /// <summary>
        /// Waits for all current tasks to finish executing before continuing.
        /// </summary>
        public void FinishTasks(List<RemeshReport> reports)
        {
            while (m_ActiveTasks.Count > 0)
            {
                var task = m_ActiveTasks[0];
                m_ActiveTasks.RemoveAt(0);

                var report = task.ToReport();
                reports.Add(report);
            }
        }

        /// <summary>
        /// Adds a remesh distributor to this handler.
        /// </summary>
        /// <param name="distributor">The distributor to add.</param>
        public void AddDistributor(IRemeshDistributor distributor)
        {
            if (distributor == null)
                return;

            if (m_Distributors.Contains(distributor))
                return;

            m_Distributors.Add(distributor);
        }

        /// <summary>
        /// Removes a remesh distributor from this handler.
        /// </summary>
        /// <param name="distributor">The distributor to remove.</param>
        public void RemoveDistributor(IRemeshDistributor distributor)
        {
            if (distributor == null)
                return;

            m_Distributors.Remove(distributor);
        }
    }
}
