using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FrameStore.Data
{
    [Table("FrameTracingRadii")]
    public class FrameTracingRadii
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FrameTracingRadiusId { get; set; }

        public Guid FrameId { get; set; }
        public Frame Frame { get; set; }
        public Double Radius { get; set; }
    }
}
