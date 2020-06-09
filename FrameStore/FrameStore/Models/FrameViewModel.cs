using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FrameStore.Data;

namespace FrameStore.Models
{
    /// <summary>
    /// Dto for a frame.
    /// </summary>
    public class FrameViewModel
    {
        /// <summary>
        /// Unique Id of the frame.
        /// </summary>
        public Guid FrameId { get; set; }

        /// <summary>
        /// Name of the brand.
        /// </summary>
        [MaxLength(50)]
        public string Brand { get; set; }

        /// <summary>
        /// Name of the collection.
        /// </summary>
        [MaxLength(50)]
        public string Collection { get; set; }

        /// <summary>
        /// Name of the style
        /// </summary>
        [MaxLength(100)]
        public string Style { get; set; }

        /// <summary>
        /// Color of the frame.
        /// </summary>
        [MaxLength(75)]
        public string FrameColor { get; set; }

        [MaxLength(50)]
        public string SKU { get; set; }

        /// <summary>
        /// Bridge number
        /// </summary>
        [Range(0, 100)] //Should check with customer
        public double Bridge { get; set; }

        /// <summary>
        /// Horizontal number
        /// </summary>
        [Range(0, 100)] //Should check with customer
        public double Horizontal { get; set; }

        /// <summary>
        /// Vertical number.
        /// </summary>
        [Range(0, 100)] //Should check with customer
        public double Vertical { get; set; }

        /// <summary>
        /// Frame material
        /// </summary>
        public List<Material> Materials { get; set; }

        /// <summary>
        /// Tracing Radii
        /// </summary>
        public List<double> TracingRadii { get; set; }
    }
}