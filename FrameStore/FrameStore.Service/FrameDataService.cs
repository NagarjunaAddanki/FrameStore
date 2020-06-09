using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrameStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FrameStore.Service
{
    ///<inheritdoc cref="IFrameDataService"/>
    public class FrameDataService : IFrameDataService
    {
        /// <summary>
        /// ASP.Net core service provider.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public FrameDataService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        ///<inheritdoc/>
        public async Task<List<Frame>> GetFramesAsync()
        {
            await using var db = _serviceProvider.GetRequiredService<FrameStoreContext>();
            return await db.Frames
                .Include(h => h.Style.Collection.Brand)
                .OrderBy(f => f.Style.Collection.Brand.Name)
                .ThenBy(f => f.Style.Collection.Name)
                .ThenBy(f => f.Style.Name)
                .ThenBy(f => f.FrameColor)
                .ToListAsync();
        }

        ///<inheritdoc/>
        public async Task DeleteFrame(Guid frameId)
        {
            await using var db = _serviceProvider.GetRequiredService<FrameStoreContext>();
            var frameToDelete = await db.Frames.FirstOrDefaultAsync(f => f.FrameId == frameId);
            if (null != frameToDelete)
            {
                db.Frames.Remove(frameToDelete);
                await db.SaveChangesAsync();
            }
        }

        ///<inheritdoc/>
        public async Task<Frame> GetFrameAsync(Guid frameId)
        {
            await using var db = _serviceProvider.GetRequiredService<FrameStoreContext>();
            return await db.Frames
                .Include(h => h.Style.Collection.Brand)
                .Include(h => h.TracingRadii)
                .Include(h => h.Materials).ThenInclude(m => m.Material)
                .FirstOrDefaultAsync(f => f.FrameId == frameId);
        }
    }
}