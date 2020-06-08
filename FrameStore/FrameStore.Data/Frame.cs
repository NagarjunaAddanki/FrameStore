using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FrameStore.Data
{
    /// <summary>
    /// Class representing a frame of a style.
    /// </summary>
    [Table("Frames")]
    public class Frame
    {
        /// <summary>
        /// Unique Id of the frame.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FrameId { get; set; }

        /// <summary>
        /// Color of the frame.
        /// </summary>
        [Required]
        [MaxLength(75)]
        public string FrameColor { get; set; }

        [MaxLength(50)]
        public string SKU { get; set; }

        /// <summary>
        /// Bridge number
        /// </summary>
        [Required]
        [Range(0,100)] //Should check with customer
        public double Bridge { get; set; }

        /// <summary>
        /// Horizontal number
        /// </summary>
        [Required]
        [Range(0, 100)] //Should check with customer
        public double Horizontal { get; set; }

        /// <summary>
        /// Vertical number.
        /// </summary>
        [Required]
        [Range(0, 100)] //Should check with customer
        public double Vertical { get; set; }

        /// <summary>
        /// List of materials
        /// </summary>
        public List<FrameMaterial> Materials { get; } = new List<FrameMaterial>();

        /// <summary>
        /// List of Tracing Radius
        /// </summary>
        public List<FrameTracingRadii> TracingRadii { get; } = new List<FrameTracingRadii>();

        /// <summary>
        /// Foreign Key
        /// </summary>
        public Guid StyleId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual Style Style{ get; set; }
    }
}
