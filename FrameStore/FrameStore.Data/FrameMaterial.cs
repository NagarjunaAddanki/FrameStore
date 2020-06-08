using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FrameStore.Data
{
    [Table("FrameMaterials")]
    public class FrameMaterial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FrameMaterialId { get; set; }

        public Guid FrameId { get; set; }
        public Frame Frame { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
