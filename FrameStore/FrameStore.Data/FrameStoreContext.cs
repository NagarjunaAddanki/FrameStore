using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FrameStore.Data
{
    public class FrameStoreContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public FrameStoreContext(DbContextOptions<FrameStoreContext> options): base(options)
        {
        }

        /// <summary>
        /// List of frames in the store.
        /// </summary>
        public DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// List of frames in the store.
        /// </summary>
        public DbSet<Frame> Frames { get; set; }

        /// <summary>
        /// List of materials used in frames
        /// </summary>
        public DbSet<Material> Materials { get; set; }
    }
}
