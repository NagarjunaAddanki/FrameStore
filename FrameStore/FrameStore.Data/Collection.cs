using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FrameStore.Data
{
    /// <summary>
    /// Class representing collection in a frame brand.
    /// </summary>
    [Table("Collections")]
    public class Collection
    {
        /// <summary>
        /// Unique Id of the brand collection
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CollectionId { get; set; }

        /// <summary>
        /// Name of the collection.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// List of styles in this collection
        /// </summary>
        public List<Style> Styles { get; } = new List<Style>();

        /// <summary>
        /// Foreign Key
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual Brand Brand { get; set; }
        
    }
}
