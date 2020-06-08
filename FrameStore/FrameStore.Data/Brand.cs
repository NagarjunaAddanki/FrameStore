using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameStore.Data
{
    /// <summary>
    /// Class representing a particular frame brand.
    /// </summary>
    [Table("Brands")]
    public class Brand
    {
        /// <summary>
        /// Unique Id of the brand.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BrandId { get; set; }

        /// <summary>
        /// Name of the brand.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Collections belonging to this brand.
        /// </summary>
        public virtual List<Collection> Collections { get; } = new List<Collection>();
    }
}
