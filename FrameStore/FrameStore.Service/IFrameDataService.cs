using FrameStore.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrameStore.Service
{
    /// <summary>
    /// Interface to access frames data.
    /// </summary>
    public interface IFrameDataService
    {
        /// <summary>
        /// Gets a list of all the frames from the store.
        /// </summary>
        /// <returns>List of all the frames.</returns>
        Task<List<Frame>> GetFramesAsync();

        /// <summary>
        /// Gets the frame from the store for a given Id.
        /// </summary>
        /// <returns>Frame instance.</returns>
        Task<Frame> GetFrameAsync(Guid frameId);

        /// <summary>
        /// Delete the frame from the store.
        /// </summary>
        /// <param name="frameId">Id of the frame to delete</param>
        /// <returns>Task to await on.</returns>
        Task DeleteFrame(Guid frameId);
    }
}