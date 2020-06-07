using System;
using System.Collections.Generic;
using System.Text;

namespace FrameStore.Data.Migrations
{
    /// <summary>
    /// Class representing individual columns in the CSV seed file.
    /// </summary>
    internal class CSVFrameData
    {
        public string SKU { get; set; }
        public string BrandName { get; set; }
        public string CollectionName { get; set; }
        public string StyleName { get; set; }
        public string FrameColor { get; set; }
        public double Bridge { get; set; }
        public double Horizontal { get; set; }
        public double Vertical { get; set; }
        public string Material { get; set; }
        public string TracingRadii { get; set; }
    }
}
