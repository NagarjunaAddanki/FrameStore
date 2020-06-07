using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FrameStore.Data
{
    /// <summary>
    /// Style in a given frame collection
    /// </summary>
    [Table("Styles")]
    public class Style
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StyleId { get; set; }

        /// <summary>
        /// Name of the style
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Frames belonging to this style.
        /// </summary>
        public virtual List<Frame> Frames { get; } = new List<Frame>();

        /// <summary>
        /// Foreign Key
        /// </summary>
        public Guid CollectionId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual Collection Collection{ get; set; }
    }
}
