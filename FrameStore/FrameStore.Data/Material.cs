using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FrameStore.Data
{
    [Table("Materials")]
    public class Material
    {
        /// <summary>
        /// Unique Id of the Material
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Name of the material
        /// </summary>
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
    }
}
